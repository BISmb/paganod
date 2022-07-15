using Paganod.Data.Shared.Interfaces.Repos;
using Paganod.Data.Shared.Interfaces;

using System;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using System.Threading.Tasks;
using Paganod.Types.Domain;

namespace Paganod.Data.Contexts.App;

internal partial class AppDbContext : IAppDbContext
{
    //public ITargetTableDataOperations this[string table, int version = -1] => throw new NotImplementedException();
    //public IDatabaseSchemaOperations Schema => new DatabaseSchemaOperations(this, AppDbConnectionFactory.NewConnection());
    //public IRecordRepository Data => new DataRepository(AppDbConnectionFactory.NewConnection(), Schema.Read);
    public IDbConnectionFactory AppDbConnectionFactory { get; }

    public virtual ISchemaModelRepo SchemaModels { get; init; }
    public ISchemaColumnRepo SchemaColumns { get; }
    public ISchemaRelationshipRepo SchemaRelationships { get; }
    public ISchemaMigrationRepo SchemaMigrations { get; }
    public ISchemaMigrationOperationRepo SchemaMigrationOperations { get; }

    public Task ExecuteMigrationOnTargetAsync(SchemaMigration forwardMigration, IAppDbContext targetDatabase = null)
    {
        throw new NotImplementedException();
    }

    //public IAttachmentRepo Attachments { get; }
    //public IManagedFileRepo ManagedFiles { get; }
    //public ISolutionRepo Solutions { get; }
    //public ISolutionItemRepo SolutionItems { get; }
    //public IConnectorRepo Connectors { get; }
    //public IConnectorArgumentRepo ConnectorArguments { get; }
    //public IConnectorDefinitionRepo ConnectorTemplates { get; }
    //public IConnectorArgumentDefinitionRepo ConnectorTemplateArguments { get; }
    //public IApiEndpointRepo ApiEndpoints { get; }
    //public IApiMethodGroupRepo ApiMethodGroups { get; }
    //public IApiEndpointArgumentRepo ApiEndpointArguments { get; }
    //public IProcesseRepo Processes { get; }
    //public IProcessTaskRepo ProcessSteps { get; }
    //public IProcessVariableRepo ProcessVariables { get; }
    //public IVariableDefinitionRepo TemplateVariables { get; }
    //public IConnectorTemplateTaskRepo ConnectorTemplateTasks { get; }
    //public IJobScheduleRepository JobSchedules { get; }
    //public IJobRepository Jobs { get; }
}

public class PaganodDbConnectionFactory : IDbConnectionFactory
{
    private readonly Type _dbConnectionType;
    private readonly string _connectionString;

    public PaganodDbConnectionFactory(Type prmDbConnectionType, string prmConnectionString)
    {
        _dbConnectionType = prmDbConnectionType;
        _connectionString = prmConnectionString;
    }

    public DbConnection NewConnection()
    {
        return _dbConnectionType.Name switch
        {
            nameof(SqliteConnection) => new SqliteConnection(_connectionString),

            _ => throw new Exception($"Cannot create a database connection of {_dbConnectionType.Name}")
        };
    }
}