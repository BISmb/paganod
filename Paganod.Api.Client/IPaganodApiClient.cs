using System;
using Paganod.Api.Shared.Feature.Config.Schema.Responses;
using Paganod.Shared.Types;

namespace Paganod.Api.Client;

public interface IPaganodApiClient
{
    Task<Record[]> GetAsync(string tableName, int pageNumber, int countPerPage);
    Task<Record?> GetAsync(string tableName, Guid recordId);
    Task<Record> SaveAsync(Record record);

    Task<SchemaModelDto[]> GetSchemaModels();
    Task<SchemaModelDto> GetSchemaModelAsync(string tableName);
    Task<SchemaModelDto> GetSchemaModelAsync(Guid schemaId);

    //Task AtatchFile(string tableName, Guid recordId, params IFormFile[] formFiles);
}


