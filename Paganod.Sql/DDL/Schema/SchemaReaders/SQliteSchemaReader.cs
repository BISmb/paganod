using Microsoft.Data.Sqlite;
using Paganod.Data.Shared.Interfaces;
using Paganod.Sql.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Paganod.Sql.DDL.Schema.SchemaReaders;

public interface ISqlSchemaReader
{
    string[] GetTableNames();
    string[] GetColumnNames(string tableName);
    string GetPrimaryKeyName(string tableName);
}

internal class SQliteSchemaReader : ISqlSchemaReader
{
    private SqliteConnection _DbConnection;

    internal SQliteSchemaReader(SqliteConnection dbConnection)
    {
        _DbConnection = dbConnection;
        // $"SELECT name FROM PRAGMA_TABLE_INFO({tableName});"
    }

    public string[] GetColumnNames(string tableName)
    {
        tableName = Regex.Escape(tableName);
        string sql = $"SELECT name FROM PRAGMA_TABLE_INFO('{tableName}');";
        return _DbConnection.GetRecords(sql).Select(x => $"{x["name"]}").ToArray();
    }

    public string GetPrimaryKeyName(string tableName)
    {
        tableName = Regex.Escape(tableName);
        string sql = $"SELECT name FROM PRAGMA_TABLE_INFO('{tableName}') WHERE pk = 1";

        if (_DbConnection.GetRecords(sql).First().TryGetValue("name", out object primaryKeyName))
            return $"{primaryKeyName}";
        else
            throw new Exception($"Cannot find primary key name for {tableName}");
    }

    public string[] GetTableNames()
    {
        string sql = "SELECT name FROM sqlite_schema WHERE type ='table' AND name NOT LIKE 'sqlite_%';";
        return _DbConnection.GetRecords(sql).Select(x => $"{x["name"]}").ToArray();
    }
}
