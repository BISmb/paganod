using Paganod.Types.Base.Paganod;
using Paganod.Types.Base.Paganod.Schema;
using Paganod.Types.Domain;
using System.Linq.Expressions;

namespace Paganod.Data.Shared.Interfaces.Repos;

/*
* Please add an async version of each method. If there is no implementation, then just call the syncronuc method
*/

public interface IExternalRepoBase<TDomainType>
{
    TDomainType Get(Guid id);
    TDomainType GetFull(Guid id);
    IEnumerable<TDomainType> GetAll();
    IEnumerable<TDomainType> GetAllFull();
    bool Exists(Guid id);

    void Add(TDomainType domainObject);
    void AddRange(IEnumerable<TDomainType> domainObjects);
    void Remove(Guid id);
    void Remove(TDomainType domainObject);
    void RemoveRange(IEnumerable<TDomainType> domainObjects);
    int Count();
}

public interface ISchemaModelRepo : IExternalRepoBase<SchemaModel>
{
    bool Exists(string sqlTableName);
    SchemaModel GetByTableName(string tableName);
    string GetTableNameFromSchemaId(Guid schemaId);
    bool Any(Expression<Func<ISchemaModel, bool>> predicate);
    SchemaModel GetByRecordType(string recordType);
    IEnumerable<string> GetAllTableNames();
    string GetPrimaryKeyForTable(string tableName);
}

public interface ISchemaColumnRepo : IExternalRepoBase<SchemaColumn>
{
    //bool Exists(string sqlTableName, string sqlColName);
    bool Exists(string tableName, string colName);
    SchemaColumn GetByColumnName(string columnName);
    SchemaColumn GetByTableNameAndColumnName(string tableName, string columnName);
    IEnumerable<SchemaColumn> GetSchemaColumnsForTableName(string tableName);
    IEnumerable<SchemaColumn> GetSchemaColumnsForSchemaModelId(Guid schemaId);
    IEnumerable<SchemaColumn> GetByAliasName(string colName);
}

public interface ISchemaRelationshipRepo : IExternalRepoBase<SchemaRelationship>
{
    IEnumerable<SchemaRelationship> GetSchemaRelationshipsForSchemaModelId(Guid id);
}

public interface ISchemaMigrationRepo : IExternalRepoBase<SchemaMigration>
{
    IEnumerable<SchemaMigration> GetUnappliedMigrations(SchemaMigrationType migrationType);
}

public interface ISchemaMigrationOperationRepo : IExternalRepoBase<SchemaMigrationOperation>
{

}