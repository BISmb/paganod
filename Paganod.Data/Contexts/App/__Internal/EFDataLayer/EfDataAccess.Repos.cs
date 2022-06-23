using Microsoft.EntityFrameworkCore;
//using Paganod.Data.Schema.App.Process;
using Paganod.Data.Schema.App.Schema;
using Paganod.Data.Schema.Files;
using Paganod.Data.Schema.Paganod.Files;
using Paganod.Data.Schema.Paganod.Schema;

namespace Paganod.Data.App.Internal;

public partial class EfDataAccess : DbContext
{
    #region Schema

    internal DbSet<SchemaModelRecord> SchemaModelRecords { get; set; }
    internal DbSet<SchemaColumnRecord> SchemaColumnRecords { get; set; }
    internal DbSet<SchemaRelationshipRecord> SchemaRelationshipRecords { get; set; }
    internal DbSet<MigrationRecord> SchemaMigrationRecords { get; set; }
    internal DbSet<SchemaMigrationOperationRecord> SchemaMigrationOperationRecords { get; set; }

    #endregion

    #region Files

    internal DbSet<AttachmentRecord> AttatchmentRecords { get; set; }
    internal DbSet<ManagedFileRecord> ManagedFilesRecords { get; set; }

    #endregion

    #region Solutions

    //internal DbSet<SolutionRecord> SolutionRecords { get; set; }
    //internal DbSet<SolutionItemRecord> SolutionItemRecords { get; set; }

    #endregion

    #region Plugin

    //internal DbSet<ConnectorRecord> ConnectorRecords { get; set; }
    //internal DbSet<ConnectorArgumentRecord> ConnectorArgumentRecords { get; set; }
    //internal DbSet<ConnectorDefinitionRecord> ConnectorTemplateRecords { get; set; }
    //internal DbSet<ConnectorTemplateArgumentRecord> ConnectorTemplateArgumentRecords { get; set; }

    //internal DbSet<ApiEndpointRecord> ApiEndpointRecords { get; set; }
    //internal DbSet<ApiEndpointArgumentRecord> ApiEndpointArgumentRecords { get; set; }
    //internal DbSet<ApiMethodGroupRecord> ApiMethodGroupRecords { get; set; }

    #endregion

    #region Workflow

    #region User-Defined

    //internal DbSet<Process> ProcessRecords { get; set; }
    //internal DbSet<ProcessTask> ProcessStepRecords { get; set; }
    //internal DbSet<ProcessVariable> ProcessVariableRecords { get; set; }

    #endregion

    #region TemplateDefinitions

    //internal DbSet<VariableDefinition> TemplateVariableRecords { get; set; }
    //internal DbSet<ConnectorDefinitionTaskRecord> ConnectorTemplateTaskRecords { get; set; }

    #endregion

    #endregion

    //internal DbSet<JobScheduleRecord> JobScheduleRecords { get; set; }
    //internal DbSet<JobRecord> JobRecords { get; set; }
}