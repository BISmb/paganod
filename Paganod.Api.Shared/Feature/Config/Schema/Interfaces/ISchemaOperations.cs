using Paganod.Api.Shared.Feature.Config.Schema.Commands;
using Paganod.Api.Shared.Feature.Config.Schema.Responses;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Paganod.Api.Shared.Feature.Config.Schema.Interfaces;

public interface ISchemaOperations
{
    Task<Guid> CreateSchemaAsync(CreateSchemaCommand createSchemaRequest, CancellationToken token = default);
    Task ModifySchemaAsync(AlterSchemaCommand modifySchemaRequest); // for now it will generate, validate, and execute migrations in-process

    Task DeleteSchemaAsync(Guid schemaId);
    Task DeleteSchemaAsync(string tableName);

    Task<SchemaModelDto> GetSchemaAsync(Guid schemaId);
    Task<SchemaModelDto> GetSchemaAsync(string tableName);
}