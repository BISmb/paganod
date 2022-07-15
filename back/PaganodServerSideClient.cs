using System;
using System.Threading.Tasks;
using Paganod.Api.Client;
using Paganod.Api.Services.Config;
using Paganod.Api.Services.Data;
using Paganod.Api.Shared.Feature.Config.Schema.Responses;
using Paganod.Shared.Types;

namespace Paganod.Api;

public class PaganodServerSideClient : IPaganodApiClient
{
    private readonly IConfigService configService;
    private readonly IDataService dataService;

    public PaganodServerSideClient(IConfigService prmConfigService,
                                   IDataService prmDataService)
    {
        configService = prmConfigService;
        dataService = prmDataService;
    }

    public Task<Record[]> GetAsync(string tableName, int pageNumber, int countPerPage)
    {
        throw new NotImplementedException();
    }

    public Task<Record> GetAsync(string tableName, Guid recordId)
    {
        throw new NotImplementedException();
    }

    public Task<SchemaModelDto> GetSchemaModelAsync(string tableName)
    {
        return configService.GetSchemaAsync(tableName);
    }

    public Task<SchemaModelDto> GetSchemaModelAsync(Guid schemaId)
    {
        return configService.GetSchemaAsync(schemaId);
    }

    public Task<SchemaModelDto[]> GetSchemaModels()
    {
        throw new NotImplementedException();
    }

    public Task<Record> SaveAsync(Record record)
    {
        return dataService.SaveRecord(record.TableName, record);
    }
}

