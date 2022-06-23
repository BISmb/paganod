//using Paganod.Data.Shared.Interfaces;
//using Paganod.Shared;

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Paganod.Sql.Utility;

//public class SchemaCache : IPaganodSchemaMap
//{
//    private readonly IAppDbContext Db;
//    private SchemaMap _schemaMap;

//    public SchemaCache(IAppDbContext prmDbContext)
//    {
//        Db = prmDbContext;
//    }

//    public bool ContainsColumn(string tableName, string columnName)
//    {
//        return _schemaMap[tableName].Contains(columnName);
//    }

//    public bool ContainsTable(string tableName)
//    {
//        return _schemaMap.ContainsKey(tableName);
//    }

//    public IEnumerable<string> GetColumnNames(string tableName)
//    {
//        return _schemaMap[tableName];
//    }

//    public (string Name, string Alias)[] GetColumnsWithVersion(string tableName, int tableVersion)
//    {
//        throw new NotImplementedException();
//    }

//    public Enums.Data.DatabaseProvider GetDatabaseProvider()
//    {
//        throw new NotImplementedException();
//    }

//    public string GetPrimaryKeyNameForTable(string tableName)
//    {
//        return _schemaMap.GetPrimaryKeyForTable(tableName);
//    }

//    public string GetTableNameFromRecordType(string recordType)
//    {
//        throw new NotImplementedException();
//    }

//    public IEnumerable<string> GetTableNames()
//    {
//        return _schemaMap.Keys;
//    }

//    public bool IsTableVersioned(string tableName)
//    {
//        throw new NotImplementedException();
//    }

//    public void Refresh()
//    {
//        using (var dbConnection = Db.NewConnection().AndOpen())
//            _schemaMap = dbConnection.GetSchemaMap();
//    }

//    (string Name, string Alias)[] ISchemaMap.GetColumnNames(string tableName)
//    {
//        throw new NotImplementedException();
//    }
//}
