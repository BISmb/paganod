using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using Paganod.Data.App.Internal;
using Paganod.Data.Contexts.App.__Internal;
using Paganod.Data.Contexts.App.Internal;
using Paganod.Data.Contexts.Records;
using Paganod.Data.Records.Repo;
using Paganod.Data.Records.Schema;
using Paganod.Data.Repos.Paganod;
using Paganod.Data.Shared.Interfaces;
using Paganod.Data.Shared.Interfaces.Repos;
using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace Paganod.Data.Contexts.App;

internal partial class AppDbContext //: IAppDbContext
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

    public async Task<IRecordDbContext> GetUserDatabaseAsync()
    {
        var db = new RecordDbContext(this);
        await Task.Delay(0);
        return db;
    }
}

//public class UserDatabase : IAppDbConnection
//{
//    private readonly IAppDbContext _paganodContext;
//    public UserDatabase(IAppDbContext prmPaganodDbContext)
//    {
//        _paganodContext = prmPaganodDbContext;
//    }

//    public IDatabaseSchemaOperations Schema => GetDatabaseOperations();
//    public IRecordRepository Data => GetDataRepository();

//    public void Dispose()
//    {
//        _paganodContext.Dispose();
//    }

//    public DbConnection NewConnection()
//    {
//        // construct database connection
//    }

//    private IDatabaseSchemaOperations GetDatabaseOperations()
//    {
//        var dbOperationStructures = new DatabaseSchemaOperations(_paganodContext, NewConnection);
//        return dbOperationStructures;
//    }

//    private IRecordRepository GetDataRepository()
//    {
//        var dataRepository = new DataRepository(Schema.Read, NewConnection);
//        return dataRepository;
//    }
//}


//public enum DatabaseContextType
//{
//    Paganod,
//    Records
//}

//public interface IPaganodAppContext
//{
//    IDisposable GetPaganodContext();
//    DbConnection GetDatabase(Guid databaseId);
//}

//public class RecordDatabase
//{
//    // Schema Operations

//    // Record Operations

//    // private metadata property (can be a map between paganod database)

//}