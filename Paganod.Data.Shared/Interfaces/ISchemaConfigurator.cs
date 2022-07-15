using Paganod.Types.Base.Paganod;
using Paganod.Types.Domain;
using System.Data;

namespace Paganod.Data.Shared.Interfaces;

public interface IPaganodSchemaConfigurator : ISchemaConfigurator
{
    Task<(SchemaMigration ForwardMigration, SchemaMigration SunsetMigration)> GeneratePaganodMigrationsAsync();
}

public interface IDatabaseSchemaOperations
{
    ISchemaReader Read { get; }
    IPaganodSchemaConfigurator Configure { get; }
}

public interface ISchemaConfigurator : IDisposable
{
    ISchemaConfigurator ForTable(string tableName);

    //Task<(SchemaMigration ForwardMigration, SchemaMigration SunsetMigration)> GeneratePaganodMigrationsAsync();
    //Task<bool> ValidatePreMigrationAsync(SchemaMigration migration);
    ISchemaConfigurator CreateTable(string tableName, string primaryKeyName = null, DbType? primaryKeyType = null);
    ISchemaConfigurator RenameTable(string tableName, string newTableName);
    ISchemaConfigurator RemoveTable(string tableName);
    ISchemaConfigurator AddColumn(string colName, FormFieldType fieldType, bool isRequired = false, object defaultValue = null, Dictionary<string, string> options = null, string alias = null, string alternativeTableName = null);
    //ISchemaConfigurator RemoveColumn(Guid colId);
    ISchemaConfigurator RemoveColumn(string colName);
    //ISchemaConfigurator AlterColumn(Guid columnId, FormFieldType type);
    ISchemaConfigurator AlterColumn(string columnName, FormFieldType fieldType);
    //ISchemaConfigurator RenameColumn(Guid columnId, string newName);
    ISchemaConfigurator RenameColumn(string columnName, string newColumnName);
    ISchemaConfigurator AddRelationship(RelationshipType relationshipType, string referencedTable, ReferentialIntegrity referentialIntegrity = ReferentialIntegrity.None, string alternativePrincipalTable = null);
}