using Fluxor;

using Paganod.Web.Client.Types;
using Paganod.Web.Store.MetadataUseCase;
using Paganod.Web.Store.MetadataUseCase.Actions;
using Paganod.Web.Store.SchemaUseCase.Actions;

namespace Paganod.Web.Store.MetadataUseCase;

public static class Reducers
{
    [ReducerMethod]
    public static MetadataState ReduceRefreshMetadataAction(MetadataState state, RefreshMetadataAction action)
    {
        return state with { IsLoading = true, StatusMessage = "Refreshing metadata..." };
    }

    [ReducerMethod]
    public static MetadataState ReduceRefreshMetadataResultAction(MetadataState state, RefreshMetadataResultAction action)
    {
        return state with { 
            IsLoading = false, 
            SchemaModelsMetadata = action.SchemaModelsMetadata,
            TableDataNavigationLinks = action.SchemaModelsMetadata.Select(metadata => $"data/{metadata.TableName}").ToArray(),
            TableConfigNavigationLinks = action.SchemaModelsMetadata.Select(metadata => $"config/schema/{metadata.SchemaId}").ToArray(),
        };
    }
}