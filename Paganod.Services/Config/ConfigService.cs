using Paganod.Api.Shared.Feature.Config.Schema.Responses;
using Paganod.Data.Shared.Interfaces;
using Paganod.Types.Api.Config.Commands;
using Paganod.Types.Domain;

namespace Paganod.Services;

public partial interface IConfigService
    : IDisposable
    , ISchemaOperations
//, ISolutionOperations
//, IPluginOperations
{

}

public interface ISchemaOperations
{
    Task CreateSchemaAsync(CreateSchemaCommand createSchemaRequest, CancellationToken token = default);
    Task ModifySchemaAsync(AlterSchemaCommand modifySchemaRequest); // for now it will generate, validate, and execute migrations in-process

    Task DeleteSchemaAsync(Guid schemaId);
    Task DeleteSchemaAsync(string tableName);

    Task<SchemaModelDto> GetSchemaAsync(Guid schemaId);
    Task<SchemaModelDto> GetSchemaAsync(string tableName);
}

public class ConfigService
{
    private readonly IAppDbContext _appDbContext;

    public static class Exceptions
    {
        public class TableAlreadyExists
        {
            public string TableName;
        }

        public class DbConnectionError { };
    }

    internal ConfigService(IAppDbContext prmAppDbContext)
    {
        _appDbContext = prmAppDbContext;
    }

    public void Dispose()
    {
        //throw new NotImplementedException();
    }

    public async Task<Guid> CreateSchemaAsync(CreateSchemaCommand request, CancellationToken token = default)
    {
        SchemaMigration migration = await GetCreateSchemaMigrationAsync(request, token);

        //if (migration.AppliedOn is not null)
        //    return new ErrorResponse("Schema Migration already applied");


        var userDatabase = await _appDbContext.GetUserDatabaseAsync();
        //await _appDbContext.ExecuteMigrationOnTargetAsync(migration, userDatabase);

        var schemaId = _appDbContext.SchemaModels.GetByTableName(request.TableName).Id;
        return schemaId;
    }

    private async Task<SchemaMigration> GetCreateSchemaMigrationAsync(CreateSchemaCommand request, CancellationToken token = default)
    {
        var userDatabase = await _appDbContext.GetUserDatabaseAsync();
        using var schemaConfigBuilder = userDatabase.Schema.Configure;

        schemaConfigBuilder.CreateTable(request.TableName, request.PrimaryKeyName, request.PrimaryKeyType);

        //if (request.NewColumns is not null && request.NewColumns.Length > 0)
        //    foreach (var col in request.NewColumns)
        //        schemaConfigBuilder.AddColumn(col.Name, col.Type, options: col.Options);

        //var generatedMigrations = await schemaConfigBuilder.GenerateMigrationsAsync();

        //// crerating a new schema will only have a forward migration
        //return generatedMigrations.ForwardMigration;

        throw new NotImplementedException();
    }

    public Task ModifySchemaAsync(AlterSchemaCommand modifySchemaRequest)
    {
        var schemaConfigurator = _appDbContext.GetSchemaConfigue();

        foreach (var col in modifySchemaRequest.AddedColumns)
            schemaConfigurator.AddColumn(col.Name, col.Type, options: col.Options);

        //foreach (var col in modifySchemaRequest.AlteredColumns)
        //    schemaConfigurator.AlterColumn(col.ColumnId, col.Type);

        //foreach(var col in modifySchemaRequest.RenamedColumns)
        //    schemaConfigurator.RenameColumn(col.ColumnId, col.NewName);

        //foreach (var colId in modifySchemaRequest.DeletedColumns)
        //    schemaConfigurator.RemoveColumn(colId);

        throw new NotImplementedException();
    }

    public Task DeleteSchemaAsync(Guid schemaId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteSchemaAsync(string tableName)
    {
        throw new NotImplementedException();
    }

    public async Task<SchemaModelDto> GetSchemaAsync(Guid schemaId)
    {
        await Task.Delay(0); // move this down to repository level

        var schema = _appDbContext.SchemaModels.GetFull(schemaId);

        var response = new SchemaModelDto()
        {
            TableName = schema.TableName,
            PrimaryKeyName = schema.PrimaryKeyName,
            PrimaryKeyType = schema.PrimaryKeyType,
            Columns = schema.Columns.Select(x => new SchemaColumnDto(x.Name, x.Type, x.Id, new Dictionary<string, string>())).ToArray(),
        };

        return response;
    }

    public Task<SchemaModelDto> GetSchemaAsync(string tableName)
    {
        var schema = _appDbContext.SchemaModels.GetByTableName(tableName);
        return GetSchemaAsync(schema.Id);
    }
}

