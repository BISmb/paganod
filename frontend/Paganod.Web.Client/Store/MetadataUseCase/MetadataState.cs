using Fluxor;

using Paganod.Web.Client.Types;

namespace Paganod.Web.Store.MetadataUseCase;

[FeatureState]
public record MetadataState : StateWithBackgroundProcessing
{
    public SchemaMetadataNavigationObject[] SchemaModelsMetadata { get; init; }
    public string[] TableDataNavigationLinks { get; init; }
    public string[] TableConfigNavigationLinks { get; init; }

    public MetadataState()
    {
        //TableNames = new string[0];
    }
}