using Paganod.Api.Shared.Feature.Config.Schema.Responses;

namespace Paganod.Web.Client.Store.SchemaUseCase;

public class FetchSchemasResultAction
{
    public SchemaModelDto[] Schemas { get; init; }

    public FetchSchemasResultAction(SchemaModelDto[] prmSchemas)
    {
        Schemas = prmSchemas;
    }
}