using System;
using Fluxor;
using Paganod.Api.Client;
using Paganod.Api.Shared;
using Paganod.Web.Client.Store.AppUseCase.Actions;
using Paganod.Web.Store.DataUseCase.Actions;

namespace Paganod.Web.Client.Store.DataUseCase.Effects;

public class FetchRecordEffect : Effect<FetchRecordAction>
{
    private readonly IPaganodApiClient _apiClient;
    private readonly ILogger<FetchRecordsEffect> _logger;

    public FetchRecordEffect(IPaganodApiClient paganodApiClient,
                              ILogger<FetchRecordsEffect> logger)
    {
        _apiClient = paganodApiClient;
        _logger = logger;
    }

    public override async Task HandleAsync(FetchRecordAction action, IDispatcher dispatcher)
    {
        try
        {
            // todo: polly resilience, error handling, etc.
            var record = await _apiClient.GetAsync(action.TableName, action.RecordId);

            if (record is null)
                dispatcher.Dispatch(new ShowErrorAction("Record was not found"));

            dispatcher.Dispatch(new FetchRecordResultAction(record));
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new ShowErrorAction(ex));
        }
    }
}

