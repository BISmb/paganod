//using Paganod.Data.Shared.Interfaces;

//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;

//namespace Paganod.Sql.SchemaReader;

//public struct TableMetadata
//{
//    public string Name { get; set; }
//    public string PrimaryKeyName { get; set; }

//    public ICollection<ColumnMetadata> Columns { get; set; }

//    public TableMetadata(string newName, string newPrimaryKey)
//    {
//        Name = newName;
//        PrimaryKeyName = newPrimaryKey;
//        Columns = new List<ColumnMetadata>();
//    }

//    public void AddColumn(string colName)
//    {
//        Columns.Add(new ColumnMetadata { Name = colName });
//    }
//}

//public struct ColumnMetadata
//{
//    public string Name { get; set; }
//}

//public class DatabaseSchemaReader : ISchemaMap
//{
//    private readonly DatabaseProvider _DatabaseType;
//    private readonly ICollection<TableMetadata> _Tables;

//    public DatabaseSchemaReader(IDbConnection dbConnection)
//    {
//        _Tables = new List<TableMetadata>();
//        _DatabaseType = DatabaseProvider.MySQL; // _DbContext.DatabaseType; // null

//        // debugging
//        var transactionTableMetadata = new TableMetadata
//        {
//            Name = "transactions",
//            PrimaryKeyName = "transaction_id",
//            Columns = new ColumnMetadata[]
//            {
//                new ColumnMetadata { Name = "transaction_id" },
//                new ColumnMetadata { Name = "columnA" },
//                new ColumnMetadata { Name = "columnB" },
//                new ColumnMetadata { Name = "columnC" },
//            }.ToList(),
//        };

//        _Tables.Add(transactionTableMetadata);

//        //RefreshSchema();
//    }

//    // https://csharp.hotexamples.com/examples/Npgsql/NpgsqlConnection/GetSchema/php-npgsqlconnection-getschema-method-examples.html
//    public void RefreshSchema()
//    {
//        // Refresh Schema
//        _Tables.Clear();
//        //_Tables.AddRange(GetMetadataInformationFromDatabase(_DbContext.NewConnection()));
//    }

//    //private IEnumerable<TableMetadata> GetMetadataInformationFromDatabase(IDbConnection dbConnection)
//    //{
//    //    TableMetadata[] tables;

//    //    var sql = "SELECT name FROM sqlite_master WHERE type='table'";

//    //    foreach(var row in tableResultRows)
//    //    {

//    //    }
//    //}

//    public bool ContainsColumn(string tableName, string columnName)
//    {
//        return _Tables.First(x => x.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase)).Columns.Any(x => x.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase));
//    }

//    public bool ContainsTable(string tableName)
//    {
//        return _Tables.Any(x => x.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase));
//    }

//    public string[] GetColumnNames(string tableName)
//    {
//        return new string[0]; // _Tables.First(x => x.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase)).Columns.Select(x => (x.Name, x.Name)).ToArray();
//    }

//    public (string Name, string Alias)[] GetColumnsWithVersion(string tableName, int tableVersion)
//    {
//        throw new NotImplementedException();
//    }

//    public DatabaseProvider GetDatabaseProvider()
//    {
//        return _DatabaseType;
//    }

//    public string GetPrimaryKeyNameForTable(string tableName)
//    {
//        return _Tables.First(x => x.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase)).PrimaryKeyName;
//    }

//    public string GetTableNameFromRecordType(string recordType)
//    {
//        throw new NotImplementedException();
//    }
//    public IEnumerable<string> GetTableNames()
//    {
//        return _Tables.Select(x => x.Name.ToLower());
//    }

//    public bool IsTableVersioned(string tableName)
//    {
//        return false;
//    }

//    public void Refresh()
//    {
//        // call sql methods to refresh the schema reader
//        throw new NotImplementedException();
//    }

//    DatabaseProvider ISchemaMap.GetDatabaseProvider()
//    {
//        return _DatabaseType;
//    }
//}
