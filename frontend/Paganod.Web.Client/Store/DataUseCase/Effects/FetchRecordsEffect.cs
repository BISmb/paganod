using System;
using Fluxor;
using Paganod.Api.Client;
using Paganod.Api.Shared;
using Paganod.Web.Client.Store.AppUseCase.Actions;
using Paganod.Web.Store.DataUseCase.Actions;

namespace Paganod.Web.Client.Store.DataUseCase.Effects;

public class FetchRecordsEffect : Effect<FetchRecordsAction>
{
    private readonly IPaganodApiClient _apiClient;
    private readonly ILogger<FetchRecordsEffect> _logger;

    public FetchRecordsEffect(IPaganodApiClient paganodApiClient,
                              ILogger<FetchRecordsEffect> logger)
    {
        _apiClient = paganodApiClient;
        _logger = logger;
    }

    public override async Task HandleAsync(FetchRecordsAction action, IDispatcher dispatcher)
    {
        try
        {
            // todo: polly resilience, error handling, etc.
            var records = await _apiClient.GetAsync(action.TableName, 1, 15);
            dispatcher.Dispatch(new FetchRecordsResultAction(records));
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new ShowErrorAction(ex));
        }
    }
}

