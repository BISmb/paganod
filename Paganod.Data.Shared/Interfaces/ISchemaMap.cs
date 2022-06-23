namespace Paganod.Data.Shared.Interfaces;

public enum DatabaseProvider
{
    SqlServer,
    MySQL,
    Postgres,
    InMemoryTesting,
    Sqlite
}

public interface ISchemaMap
{
    bool ContainsTable(string tableName);
    bool ContainsColumn(string tableName, string columnName);

    IEnumerable<string> GetTableNames();
    string[] GetColumnNames(string tableName);
    string GetPrimaryKeyNameForTable(string tableName);
    DatabaseProvider GetDatabaseProvider();
    void Refresh();
}

public interface IPaganodSchemaMap : ISchemaMap
{
    string GetTableName(string recordTypeOrTableName);
    string GetTableNameFromRecordType(string recordType);
    bool IsTableVersioned(string tableName);
    (string Name, string Alias)[] GetColumnsWithVersion(string tableName, int tableVersion);
    //DatabaseProvider GetDatabaseProvider();
}

public interface ISchemaReader : IPaganodSchemaMap
{

}