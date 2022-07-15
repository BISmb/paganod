using System.Text.Json;
using Fluxor;
using Paganod.Api.Client;
using Paganod.Api.Shared;
using Paganod.Web.Client.Store.DataUseCase.Actions;
using Paganod.Web.Client.Types;
using Paganod.Web.Store.DataUseCase.Actions;
using Paganod.Web.Store.MetadataUseCase.Actions;

namespace Paganod.Web.Store.MetadataUseCase;

public class Effects
{
    private readonly IPaganodApiClient PaganodApiClient;

    public Effects(IPaganodApiClient paganodApiClient)
    {
        PaganodApiClient = paganodApiClient;
    }

    [EffectMethod]
    public async Task HandleRefreshMetadataAction(RefreshMetadataAction action, IDispatcher dispatcher)
    {
        var metadata = new SchemaMetadataNavigationObject[] 
        { 
            new SchemaMetadataNavigationObject() { SchemaId = Guid.Empty, DisplayTableName = "Transactions", TableName = "transactions" },
        };

        dispatcher.Dispatch(new RefreshMetadataResultAction(metadata));
    }
}