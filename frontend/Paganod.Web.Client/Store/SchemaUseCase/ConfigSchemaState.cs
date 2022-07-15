using Fluxor;

using Paganod.Web.Client.Types;
using Paganod.Web.Client.Types.ViewModels;
using Paganod.Web.Store.AppUseCase;

using Paganod.Api.Shared.Feature.Config.Schema.Responses;

namespace Paganod.Web.Store.SchemaUseCase;

[FeatureState]
public record ConfigSchemaState : LoadingState
{
    public SchemaModelDto[] SchemaModels { get; init; }
    public SchemaModelDto SchemaModel { get; init; }
    public EditSchemaFormViewModel Form { get; init; }

    public ConfigSchemaState()
    {
        Form = new EditSchemaFormViewModel();
    }
}