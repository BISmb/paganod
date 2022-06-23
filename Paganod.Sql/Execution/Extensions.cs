using Dapper;

using Paganod.Data.Shared;
using Paganod.Sql.Types;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Paganod.Sql.Execution;

internal class Extensions
{
    public static Task<IDictionary<string, object>> GetRowAsync(IDbConnection dbConnection, PaganodSqlQuery query)
    {
        var taskCompletionSource = new TaskCompletionSource<IDictionary<string, object>>();

        Dictionary<string, object> resultRow;

        var commandDefinition = new CommandDefinition(query.Sql);

        var args = new DynamicParameters(new { });

        foreach (var param in query.Parameters)
            args.Add(param.Key, param.Value);

        var result = dbConnection.Query(query.Sql, args);


        using (var dbCommand = dbConnection.CreateCommand())
        {
            dbCommand.CommandText = query.Sql;
            dbCommand.AddParameters(query.Parameters);

            if (dbCommand.Connection.State != ConnectionState.Open)
                dbCommand.Connection.Open();

            var reader = dbCommand.ExecuteReader(); // todo: make this async?

            while (reader.Read())
            {
                resultRow = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var colName = reader.GetName(i);
                    var colValue = reader.GetValue(i); // too: bindings
                    resultRow.Add(colName, colValue);
                }

                taskCompletionSource.SetResult(resultRow);
            }
        }

        return taskCompletionSource.Task;
    }
}
