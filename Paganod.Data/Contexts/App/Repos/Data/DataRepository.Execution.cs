using Paganod.Data.Shared;
using Paganod.Sql.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Paganod.Data.App.Repos.Data;

internal partial class DataRepository
{
    // todo: run this async?
    /// <summary>
    /// Run a PaganodSqlQuery and return the results
    /// </summary>
    /// <param name="query">A Paganod formatted sql query</param>
    /// <returns>A Dictionary that represents the row and values returned from the database</returns>
    private Task<T> ExecuteQueryAsync<T>(Func<IDbCommand, T> returnQueryResults, PaganodSqlQuery query)
    {
        var taskCompletionSource = new TaskCompletionSource<T>();

        using var dbConnection = _DbConnectionFactory.NewConnection();
        using var dbCommand = dbConnection.CreateCommand();

        dbCommand.CommandText = query.Sql;
        dbCommand.AddParameters(query.Parameters);

        if (dbCommand.Connection.State != ConnectionState.Open)
            dbCommand.Connection.Open();

        taskCompletionSource.SetResult(returnQueryResults(dbCommand));

        return taskCompletionSource.Task;
    }

    /// <summary>
    /// Execute a query that will return the first row of the results (as IDictionary|string, object|).
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    private Task<IDictionary<string, object>> QueryFirstAsync(PaganodSqlQuery query)
    {
        return ExecuteQueryAsync(GetRecord, query);
    }

    /// <summary>
    /// Execute a query that will return multiple rows (as IDictionary|string, object|).
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    private Task<IEnumerable<IDictionary<string, object>>> QueryAsync(PaganodSqlQuery query)
    {
        return ExecuteQueryAsync(GetRecords, query);
    }

    private IDictionary<string, object> GetRecord(IDbCommand dbCommand)
    {
        return GetRecords(dbCommand).FirstOrDefault();
    }

    private IEnumerable<IDictionary<string, object>> GetRecords(IDbCommand dbCommand)
    {
        var dbReader = dbCommand.ExecuteReader();

        while (dbReader.Read())
        {
            var resultRow = new Dictionary<string, object>();

            for (int i = 0; i < dbReader.FieldCount; i++)
            {
                var colName = dbReader.GetName(i);
                var colValue = dbReader.GetValue(i); // too: bindings
                resultRow.Add(colName, colValue);
            }

            yield return resultRow;
        }
    }

    // todo: run this async?
    /// <summary>
    /// Run a PaganodSqlQuery and return the results
    /// </summary>
    /// <param name="query">A Paganod formatted sql query</param>
    /// <returns>A Dictionary that represents the row and values returned from the database</returns>
    private Task<T> ExecuteScalarAsync<T>(PaganodSqlQuery query)
    {
        return ExecuteQueryAsync(ExecuteScalar<T>, query);
    }

    private T ExecuteScalar<T>(IDbCommand dbCommand)
    {
        return (T)dbCommand.ExecuteScalar();
    }

    // todo: run this async?
    /// <summary>
    /// Run a PaganodSqlQuery with new results
    /// </summary>
    /// <param name="query">A Paganod formatted sql query</param>
    /// <returns>A Dictionary that represents the row and values returned from the database</returns>
    private int ExecuteNonQuery(PaganodSqlQuery query)
    {
        using var dbConnection = _DbConnectionFactory.NewConnection();
        using var dbCommand = dbConnection.CreateCommand();

        dbCommand.CommandText = query.Sql;
        dbCommand.AddParameters(query.Parameters);
        return dbCommand.ExecuteNonQuery(); // todo: make this async?
    }
}
