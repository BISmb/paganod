using Paganod.Api.Shared.Feature.Config.Schema.Responses;

namespace Paganod.Web.Store.SchemaUseCase.Actions;

public class FetchConfigSchemaResultAction
{
    public SchemaModelDto SchemaModel { get; init; }

    public FetchConfigSchemaResultAction(SchemaModelDto newSchemaModel)
    {
        SchemaModel = newSchemaModel;
    }
}