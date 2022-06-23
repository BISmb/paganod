
using Paganod.Data.Shared.Interfaces.Repos;
using Paganod.Types.Base.Paganod.Schema;
using Paganod.Types.Domain;

using System.Data;
using System.Data.Common;

namespace Paganod.Data.Shared.Interfaces;

public interface IAppDbConnection
{
    IRecordRepository Data { get; }
    //ITargetTableDataOperations this[string table, int version = -1] { get; }

    IDatabaseSchemaOperations Schema { get; }
    DbConnection NewConnection(); // utilizes DbFactory private method under the hood
}

/// <summary>
/// Represents a single database connection, with a single transaction.
/// To be used within a using() statement
/// </summary>
public interface IAppDbContext : IDisposable, IDataContext
{
    #region "Utility Functions"
    //Enums.Data.DatabaseProvider DatabaseType { get; }
    //string DatabaseName { get; }
    //string ConnectionString { get; }
    //IDatabaseSchemaOperations Schema { get; }
    //IDbConnectionFactory AppDbConnectionFactory { get; }
    Task InitalizeAsync();
    //IDbConnection GetUserDatabase();
    #endregion

    #region Schema Configure
    Task RunUnappliedMirationsAsync(SchemaMigrationType migrationsType);
    ISchemaConfigurator GetSchemaConfigue();
    DbCommand GetDbCommand();
    #endregion

    #region Schema Models
    ISchemaModelRepo SchemaModels { get; }
    ISchemaColumnRepo SchemaColumns { get; }
    ISchemaRelationshipRepo SchemaRelationships { get; }
    ISchemaMigrationRepo SchemaMigrations { get; }
    ISchemaMigrationOperationRepo SchemaMigrationOperations { get; }
    Task ExecuteMigrationAsync(Guid migrationId);
    Task ExecuteMigrationOnTargetAsync(SchemaMigration forwardMigration, IAppDbConnection targetDatabase = null);

    //ISchemaMigrationRepo SchemaMigrations { get; }
    //ISchemaMigrationOperationRepo SchemaMigrationOperations { get; }

    #endregion

    #region Files
    //IAttachmentRepo Attachments { get; }
    //IManagedFileRepo ManagedFiles { get; }
    #endregion

    #region Solutions
    //ISolutionRepo Solutions { get; }
    //ISolutionItemRepo SolutionItems { get; }
    #endregion

    #region Plugin
    //IConnectorRepo Connectors { get; }
    //IConnectorArgumentRepo ConnectorArguments { get; }
    //IConnectorDefinitionRepo ConnectorTemplates { get; }
    //IConnectorArgumentDefinitionRepo ConnectorTemplateArguments { get; }
    //IApiEndpointRepo ApiEndpoints { get; }
    //IApiEndpointArgumentRepo ApiEndpointArguments { get; }
    //IApiMethodGroupRepo ApiMethodGroups { get; }

    // Api Endpoints

    #endregion

    #region Workflow

    #region User-Designed
    //IProcesseRepo Processes { get; }
    //IProcessTaskRepo ProcessSteps { get; }
    //IProcessVariableRepo ProcessVariables { get; }
    #endregion

    #region Template Definitions
    //IVariableDefinitionRepo TemplateVariables { get; }
    //IConnectorTemplateTaskRepo ConnectorTemplateTasks { get; }


    #endregion
    #endregion

    #region Process
    //IJobScheduleRepository JobSchedules { get; }
    //IJobRepository Jobs { get; }
    #endregion
}