using Paganod.Api.Shared.Feature.Config.Schema.Responses;

namespace Paganod.Web.Store.SchemaUseCase.Actions;

public class FetchConfigSchemaResultAction
{
    public readonly SchemaModelDto SchemaModel;

    public FetchConfigSchemaResultAction(SchemaModelDto newSchemaModel)
    {
        SchemaModel = newSchemaModel;
    }
}