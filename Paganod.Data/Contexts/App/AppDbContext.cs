using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using Paganod.Data.App.Internal;
using Paganod.Data.Contexts.App.__Internal;
using Paganod.Data.Contexts.App.Internal;
using Paganod.Data.Repos.Paganod;
using Paganod.Data.Shared.Interfaces;

using System;
using System.Data.Common;

namespace Paganod.Data.Contexts.App;

internal partial class AppDbContext : IAppDbContext
{
    internal readonly EfDataAccess DataAccessLayer;

    internal AppDbContext(DbContextOptions<EfDataAccess> dbOptions, 
                          Guid? tenantId = null, 
                          Guid? enviornmentId = null)
    {
        DataAccessLayer = tenantId is null
            ? new SingleDatabaseLayer(dbOptions)
            : new MultiTenantLayer(dbOptions);

        SchemaModels = new SchemaModelRepo(this);
        SchemaColumns = new SchemaColumnRepo(this);
        SchemaRelationships = new SchemaRelationshipRepo(this);

        //SchemaMigrations = new SchemaMigrationRepo(this);
        //SchemaMigrationOperations = new SchemaMigrationOperationRepo(this);

        //Solutions = new SolutionsRepo(this);
        //SolutionItems = new SolutionItemsRepo(this);

        //ApiEndpoints = new ApiEndpointRepo(this);
        //ApiEndpointArguments = new ApiEndpointArgumentRepo(this);
        //ApiMethodGroups = new ApiMethodGroupRepo(this);

        //ManagedFiles = new ManagedFilesRepo(this);
        //Attachments = new AttachmentsRepo(this);

        //Jobs = new JobRepository(this);

    }

    public void Dispose()
    {

    }
}
