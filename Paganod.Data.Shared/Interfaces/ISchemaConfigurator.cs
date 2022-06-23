using Paganod.Types.Base.Paganod;
using Paganod.Types.Domain;
using System.Data;

namespace Paganod.Data.Shared.Interfaces;

public interface IDatabaseSchemaOperations
{
    ISchemaReader Read { get; }
    ISchemaConfigurator Configure { get; }
}

public interface ISchemaConfigurator : IDisposable
{
    ISchemaConfigurator ForTable(string tableName);

    Task<(SchemaMigration ForwardMigration, SchemaMigration SunsetMigration)> GenerateMigrationsAsync();
    Task<bool> ValidatePreMigrationAsync(SchemaMigration migration);
    ISchemaConfigurator CreateTable(string tableName, string primaryKeyName = null, DbType? primaryKeyType = null);
    ISchemaConfigurator RenameTable(string tableName, string newTableName);
    ISchemaConfigurator RemoveTable(string tableName);
    ISchemaConfigurator AddColumn(string colName, FormFieldType fieldType, bool isRequired = false, object defaultValue = null, Dictionary<string, string> options = null, string alias = null, string alternativeTableName = null);
    ISchemaConfigurator RemoveColumn(string colName);
    ISchemaConfigurator AlterColumn(string columnName, FormFieldType fieldType);
    ISchemaConfigurator RenameColumn(string columnName, string newColumnName);
    ISchemaConfigurator AddRelationship(RelationshipType relationshipType, string referencedTable, ReferentialIntegrity referentialIntegrity = ReferentialIntegrity.None, string alternativePrincipalTable = null);
}