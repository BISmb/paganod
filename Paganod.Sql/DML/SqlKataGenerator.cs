using DynamicODataToSQL;
using Paganod.Data.Shared.Interfaces;
using Paganod.Sql.Types;
using SqlKata;
using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Paganod.Sql.DML;

public class SqlKataGenerator : IPaganodSqlKataGenerator
{
    private Compiler _KataCompiler => GetKataCompiler();
    private readonly IPaganodSchemaMap _SchemaMap;

    public SqlKataGenerator(IPaganodSchemaMap schemaMap) // this should be private
    {
        _SchemaMap = schemaMap;
    }

    /**
     * Utility Functions
     */
    #region "Utility Functions"

    private Compiler GetKataCompiler()
    {
        var dbProvider = _SchemaMap.GetDatabaseProvider();
        return dbProvider switch
        {
            DatabaseProvider.MySQL => new MySqlCompiler(),
            DatabaseProvider.Postgres => new PostgresCompiler(),
            DatabaseProvider.SqlServer => new SqlServerCompiler(),
            DatabaseProvider.Sqlite => new SqliteCompiler(),

            _ => throw new NotSupportedException($"Db Provider: {dbProvider} is not supported")
        };
    }

    private IDictionary<string, object> GetSqlParametersDictionary(object[] objectParams)
    {
        var sqlParams = new Dictionary<string, object>();
        for (int i = 0; i < objectParams.Length; i++)
            sqlParams.Add($"@p{i}", objectParams[i]);

        return sqlParams;
    }

    private PaganodSqlQuery KataQueryToPaganodQuery(Query kataQuery)
    {
        var generatedQuery = _KataCompiler.Compile(kataQuery);
        var sqlParams = GetSqlParametersDictionary(generatedQuery.Bindings.ToArray());
        return new PaganodSqlQuery(generatedQuery.Sql, sqlParams);
    }

    private (string TableName, string PrimaryKeyColumn) GetTableInfo(string tableName)
    {
        //var tableName = GetTableNameFromRecordType(recordType);
        var primaryKeyName = GetPrimaryKeyNameForTable(tableName);

        return (tableName, primaryKeyName);
    }

    private (string Name, string Alias)[] GetAllColumnsForTableName(string tableName)
    {
        var colNames = _SchemaMap.GetColumnNames(tableName);

        return colNames.Select(x => (x, x)).ToArray();
    }

    private string GetTableNameFromRecordType(string recordType)
    {
        if (_SchemaMap.ContainsTable(recordType))
            return recordType; // this is a tableName

        return _SchemaMap.GetTableNameFromRecordType(recordType);
    }

    private string GetPrimaryKeyNameForTable(string tableName)
    {
        return _SchemaMap.GetPrimaryKeyNameForTable(tableName);
    }

    private bool IsTableVersioned(string tableName)
    {
        return _SchemaMap.IsTableVersioned(tableName);
    }

    private (string Name, string Alias)[] GetAllColumnsForTableVersion(string tableName, int tableVersion)
    {
        return _SchemaMap.GetColumnsWithVersion(tableName, tableVersion);
    }

    #endregion

    public PaganodSqlQuery Get(string recordType, Guid recordId)
    {
        var info = GetTableInfo(recordType);
        var columns = GetAllColumnsForTableName(info.TableName);

        if (IsTableVersioned(info.TableName))
            columns = GetAllColumnsForTableVersion(info.TableName, 1);

        var query = new Query(info.TableName).Where(info.PrimaryKeyColumn, recordId)
                                             .Select(columns);

        return KataQueryToPaganodQuery(query);
    }

    public PaganodSqlQuery Get(string recordType, int page, int quantity)
    {
        var info = GetTableInfo(recordType);
        var columns = GetAllColumnsForTableName(info.TableName);

        if (IsTableVersioned(info.TableName))
            columns = GetAllColumnsForTableVersion(info.TableName, 1);

        var query = new Query(info.TableName).Select(columns).ForPage(page, quantity);
        return KataQueryToPaganodQuery(query);
    }

    public PaganodSqlQuery Get(string recordType, IReadOnlyDictionary<string, object> filters, int page, int quantity)
    {
        var info = GetTableInfo(recordType);
        var columns = GetAllColumnsForTableName(info.TableName);

        if (IsTableVersioned(info.TableName))
            columns = GetAllColumnsForTableVersion(info.TableName, 1);

        var query = new Query(info.TableName).Select(columns).Where(filters).ForPage(page, quantity);
        return KataQueryToPaganodQuery(query);
    }

    /// <summary>
    /// Set a columnDestination = columnSource in a table. Optionally: specif a transformation.
    /// WARNING: Will lock the table
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="columnSource"></param>
    /// <param name="columnDestination"></param>
    /// <param name="transformationExpression"></param>
    /// <returns></returns>
    public PaganodSqlQuery UpdateColumn(string tableName, string columnSource, string columnDestination, string transformationExpression = "")
    {
        var info = GetTableInfo(tableName);

        var query = new Query(info.TableName)
            .AsUpdate(new string[] { columnDestination }, new object[] { _KataCompiler.WrapValue(columnDestination) });

        return KataQueryToPaganodQuery(query);
    }

    /*
    public PaganodSqlQuery Get(ODataQuery oDataQuery)
    {
        //var compiler = new SqlServerCompiler() { UseLegacyPagination = false };

        var converter = new ODataToSqlConverter(new EdmModelBuilder(), _KataCompiler);
        var tableName = oDataQuery.BaseTableName;
        var odataQueryParams = oDataQuery.GetDynamicDictionary();

        var result = converter.ConvertToSQL(
                       tableName,
                       odataQueryParams,
                       false);

        return new PaganodSqlQuery(result.Item1, result.Item2);
    }
    */

    public PaganodSqlQuery Delete(string recordType, Guid recordId)
    {
        var info = GetTableInfo(recordType);
        var query = new Query(info.TableName).Where(info.PrimaryKeyColumn, recordId)
                                             .AsDelete();

        return KataQueryToPaganodQuery(query);
    }

    public PaganodSqlQuery Update(string recordType, Guid recordId, IReadOnlyDictionary<string, object> data)
    {
        var info = GetTableInfo(recordType);
        var query = new Query(info.TableName).Where(info.PrimaryKeyColumn, recordId)
                                             .AsUpdate(data);

        return KataQueryToPaganodQuery(query);
    }

    public PaganodSqlQuery Insert(string recordType, IReadOnlyDictionary<string, object> data)
    {
        var info = GetTableInfo(recordType);
        var query = new Query(info.TableName).AsInsert(data);

        return KataQueryToPaganodQuery(query);
    }

    public PaganodSqlQuery Count(string tableName, IDictionary<string, object> filters = null)
    {
        var info = GetTableInfo(tableName);
        var query = new Query(info.TableName).AsCount();

        if (filters?.Count > 0)
            query = query.Where(filters);

        return KataQueryToPaganodQuery(query);
    }
}

public static class SqlKataExtensions
{
    // todo: unit twst this method
    public static Query Select(this Query query, (string Name, string Alias)[] columnAndAliases)
    {
        query.Method = "select";

        var columns = columnAndAliases
            .Select(x => $"{x.Item1} as {x.Alias}")
            .ToArray();


        foreach (var column in columns)
        {
            query.AddComponent("select", new Column
            {
                Name = column
            });
        }

        return query;
    }
}