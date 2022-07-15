using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using Paganod.Data.Shared.Interfaces;
using Paganod.Shared;
using Paganod.Sql.DDL.Schema.SchemaReaders;
using Paganod.Sql.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paganod.Data.Records.Schema;

/// <summary>
/// A class that has methods to retrieve the underlying database schema
/// </summary>
internal class SchemaReader : ISchemaReader, IDisposable
{
    private readonly IDbConnection _DbConnection;
    private readonly ISqlSchemaReader _SqlSchemaReader;

    private IDictionary<string, (string PrimaryKey, string[] Columns)> Schema;

    public SchemaReader(IDbConnection dbConnection)
    {
        _DbConnection = dbConnection;
        _SqlSchemaReader = _DbConnection.GetSchemaReader();
        Refresh();
    }

    public bool ContainsColumn(string tableName, string columnName)
    {
        return Schema[tableName].Columns.Contains(columnName);
    }

    public bool ContainsTable(string tableName)
    {
        return Schema.ContainsKey(tableName);
    }

    public void Dispose()
    {
        _DbConnection.Close();
        _DbConnection.Dispose();
    }

    public (string Name, string Alias)[] GetColumnNames(string tableName)
    {
        return Schema[tableName].Columns.Select(x => (x, x)).ToArray();
    }

    public (string Name, string Alias)[] GetColumnsWithVersion(string tableName, int tableVersion)
    {
        //if (_Db is null)
        //    throw new NullReferenceException();

        //return _Db.SchemaColumns.GetSchemaColumnsForTableName(tableName)
        //    .Where(x => x.Version == tableVersion || x.Version is null)
        //    .Select(x => (x.Name, x.Alias)).ToArray();

        throw new NotImplementedException();
    }

    public DatabaseProvider GetDatabaseProvider()
    {
        return DatabaseProvider.Sqlite;
    }

    public string GetPrimaryKeyNameForTable(string tableName)
    {
        return Schema[tableName].PrimaryKey;
    }

    public string GetTableName(string recordTypeOrTableName)
{
        if (ContainsTable(recordTypeOrTableName))
            return recordTypeOrTableName; // this is a tableName

        return GetTableNameFromRecordType(recordTypeOrTableName);
    }

    public string GetTableNameFromRecordType(string recordType)
    {
        throw new NotImplementedException();
        //return _Db.SchemaModels.GetByRecordType(recordType).TableName;
    }

    public IEnumerable<string> GetTableNames()
    {
        return Schema.Keys;
    }

    public bool IsTableVersioned(string tableName)
    {
        return false; // _Db.SchemaModels.GetByTableName(tableName).Versioning;
    }

    public void Refresh()
    {
        Schema = new Dictionary<string, (string PrimaryKeyName, string[] Columns)>(StringComparer.OrdinalIgnoreCase);
        var tableNames = _SqlSchemaReader.GetTableNames();

        foreach(var table in tableNames)
        {
            var primaryKeyName = _SqlSchemaReader.GetPrimaryKeyName(table);
            var columns = _SqlSchemaReader.GetColumnNames(table);
            Schema.Add(table, (primaryKeyName, columns));
        }
    }

    string[] ISchemaMap.GetColumnNames(string tableName)
    {
        return Schema[tableName].Columns;
    }

    DatabaseProvider ISchemaMap.GetDatabaseProvider()
    {
        throw new NotImplementedException();
    }
}
