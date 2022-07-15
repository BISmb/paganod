using System;
using Fluxor;
using Paganod.Web.Client.Store.ConnectorUseCase.Actions;

namespace Paganod.Web.Client.Store.ConnectorUseCase;

public class Effects
{
    public Effects()
    {

    }

    [EffectMethod]
    public async Task HandleFetchConnectorsAction(FetchConnectorsAction action, IDispatcher dispatcher)
    {
        await Task.Delay(3000);

        var results = new ConnectorDto[]
        {
            new ConnectorDto { ConnectorId = Guid.NewGuid(), Type = "data", ConnectorJson = "{'propertyName':'fffff'}" },
            new ConnectorDto { ConnectorId = Guid.NewGuid(), Type = "data", ConnectorJson = "{'propertyName':'fffff'}" },
            new ConnectorDto { ConnectorId = Guid.NewGuid(), Type = "data", ConnectorJson = "{'propertyName':'fffff'}" },
            new ConnectorDto { ConnectorId = Guid.NewGuid(), Type = "data", ConnectorJson = "{'propertyName':'fffff'}" },
        };

        dispatcher.Dispatch(new FetchConnectorsActionResults(results));
    }
}

