using System;
using System.Text.Json;
using Fluxor;
using Paganod.Api.Client;
using Paganod.Api.Shared;
using Paganod.Web.Client.Store.AppUseCase.Actions;
using Paganod.Web.Store.DataUseCase.Actions;

namespace Paganod.Web.Client.Store.DataUseCase.Effects;

public class SaveRecordEffect : Effect<SaveRecordAction>
{
    private readonly IPaganodApiClient _apiClient;
    private readonly ILogger<SaveRecordEffect> _logger;

    public SaveRecordEffect(IPaganodApiClient paganodApiClient,
                            ILogger<SaveRecordEffect> logger)
    {
        _apiClient = paganodApiClient;
        _logger = logger;
    }

    public override async Task HandleAsync(SaveRecordAction action, IDispatcher dispatcher)
    {
        try
        {
            _logger.LogInformation(JsonSerializer.Serialize(action.Record));

            var record = await _apiClient.SaveAsync(action.Record);
            //dispatcher.Dispatch(new SetCurrentRecordAction(record));
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new ShowErrorAction(ex));
        }
    }
}
