using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using MySqlConnector;
using Paganod.Sql.DDL.Schema.SchemaReaders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;

namespace Paganod.Sql.Utility;

public static class IDbConnectionExtensions
{
    public static ISqlSchemaReader GetSchemaReader(this IDbConnection dbConnection)
    {
        return dbConnection switch
        {
            SqliteConnection => new SQliteSchemaReader(dbConnection as SqliteConnection),

            _ => throw new NotImplementedException(),
        };
    }

    /// <summary>
    /// Warning, only used as quick method for test. do not pass in user input.
    /// </summary>
    /// <param name="dbConnection"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public static IEnumerable<IDictionary<string, object>> GetRecords(this IDbConnection dbConnection, string sql)
    {
        //CleanSqlString(ref sql);

        using var dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = sql;

        if (dbConnection.State != ConnectionState.Open)
            dbConnection.Open();

        using var dbReader = dbCommand.ExecuteReader();
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

        dbConnection.Close();
    }

    private static void CleanSqlString(ref string sql)
    {
        sql = Regex.Escape(sql);
    }

    public static SchemaMap GetSchemaMap(this IDbConnection db, Func<string, bool> filter = default)
    {
        SchemaMap schema = new();

        /*
        IEnumerable<string> tableNames = db.GetTablesList();

        if (filter is not null) tableNames = tableNames.Where(filter);

        foreach (string TableName in tableNames)
        {
            schema.AddTable(TableName);

            foreach (string ColumnName in db.GetColumnsList(TableName))
            {
                schema.AddColumn(TableName, ColumnName);
            }
        }
        */

        // debug
        schema.AddTable("transactions");
        schema.AddColumn("transactions", "transaction_id");
        schema.AddColumn("transactions", "columnA");
        schema.AddColumn("transactions", "columnB");
        schema.AddColumn("transactions", "columnB");
        schema.AddRecordTypeTableMatches(new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "transaction", "transactions" }
        });

        return schema;
    }

    //public static ISqlModifyRepo OpenSchemaModifyTransaction(this IDbConnection db, string targetTable)
    //{
    //    var repo = new SqlModifyRepo(db);
    //    repo.SetTargetTable(targetTable);
    //    return repo;

    //}

    //public static KeyValuePair<string, IEnumerable<string>> ReadSchemaTable(this IDbConnection db, string tableName)
    //{
    //    List<string> columns = new List<string>();

    //    foreach (string ColumnName in db.GetColumnsList(tableName))
    //    {
    //        columns.Add(tableName, ColumnName);
    //    }

    //    return new KeyValuePair<string, IEnumerable<string>>();
    //}


    public static IEnumerable<string> GetTablesList(this IDbConnection db)
    {
        string sql = db switch
        {
            MySqlConnection => GetMySqlSchemaRead(db.Database),
            SqlConnection => GetMsSqlSchemaRead(db.Database),
            //System.Data.SqlClient.SqlConnection => GetMsSqlSchemaRead(db.Database),
            //Microsoft.Data.Sqlite.SqliteConnection => GetSqliteSchemaRead(db.Database),

            _ => throw new NotImplementedException($"Schema Read is not implented for {db.GetType().Name}")
        };

        return GetSchemaStringList(db, sql);
    }

    private static object GetSqliteSchemaRead(string database)
    {
        throw new NotImplementedException();
    }

    private static string GetMsSqlSchemaRead(params string[] prms)
    {
        string sql = "";
        sql = GetSqlServerSchemaSqlRead(SchemaReadType.Table);
        sql = sql.Replace("@DbName", prms[0]);
        return sql;
    }

    private static string GetSqlServerSchemaSqlRead(SchemaReadType readType)
    {
        switch (readType)
        {
            case SchemaReadType.Table:

                return $@"  select table_name
                            from information_schema.tables
                            where table_type = 'base table' 
                                and table_catalog = db_name();";

            case SchemaReadType.Column:

                return $@"  select column_name
                            from information_schema.columns
                            where table_name = '@TableName'
                                and table_catalog = db_name();";

            default:
                throw new Exception("SchemaReadType was not specified.");
        }
    }

    public static string GetMySqlSchemaRead(params string[] prms)
    {
        string sql = "";
        sql = GetMySqlSchemaSqlRead(SchemaReadType.Table);
        sql = sql.Replace("@DbName", prms[0]);
        return sql;
    }

    public static IEnumerable<string> GetColumnsList(this IDbConnection db, string TableName)
    {
        string sql = "";
        switch (db)
        {
            case MySqlConnection:

                sql = GetMySqlSchemaSqlRead(SchemaReadType.Column);
                sql = sql.Replace("@TableName", TableName);

                break;

            case SqlConnection:

                sql = GetSqlServerSchemaSqlRead(SchemaReadType.Column);
                sql = sql.Replace("@TableName", TableName);

                break;

            default:
                throw new NotImplementedException("Unsupported IDbConnection. Cannot read schema.");
        }

        return GetSchemaStringList(db, sql);
    }

    public enum SchemaReadType
    {
        Table,
        Column
    }

    private static string GetMySqlSchemaSqlRead(SchemaReadType readType)
    {
        switch (readType)
        {
            case SchemaReadType.Table:

                return $@"select table_name
                            from information_schema.tables
                            where table_type = 'BASE TABLE'
                                and table_schema = '@DbName'
                            order by table_name;";

            case SchemaReadType.Column:

                return $@"select column_name
                            from information_schema.columns 
                            where 
                                table_schema = Database()
                            and table_name = '@TableName';";

            default:
                throw new Exception("SchemaReadType was not specified.");
        }
    }

    private static IEnumerable<string> GetSchemaStringList(IDbConnection db, string sql)
    {
        List<string> TableNames = new();
        using var cmd = db.CreateCommand();
        //var results = db.RunCommand(sql);

        //foreach (Record row in results)
        //    TableNames.Add(row.AllData().First().Value.ToString());

        return TableNames.AsEnumerable();
    }

    //public static IDbConnection NewConnection(this IDbConnection dbConnection)
    //{
    //    DbProviderFactory factory = DbProviderFactories.GetFactory(dbConnection as DbConnection);
    //    var clonedConnection = factory.CreateConnection() as IDbConnection;
    //    clonedConnection.ConnectionString = dbConnection.ConnectionString;
    //    return clonedConnection;
    //}
}
