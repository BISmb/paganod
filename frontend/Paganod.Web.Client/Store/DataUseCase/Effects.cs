using System.Text.Json;
using Fluxor;

using Paganod.Web.Client.Services;
using Paganod.Web.Client.Store.DataUseCase.Actions;
using Paganod.Web.Store.DataUseCase.Actions;

namespace Paganod.Web.Store.DataUseCase;

public class Effects
{
    private readonly IPaganodApiClient PaganodApiClient;

    public Effects(IPaganodApiClient paganodApiClient)
    {
        PaganodApiClient = paganodApiClient;
    }

    [EffectMethod]
    public async Task HandleFetchRecordsAction(FetchRecordsAction action, IDispatcher dispatcher)
    {
        // todo: polly resilience, error handling, etc.
        var records = await PaganodApiClient.GetAsync(action.TableName, 1, 15);

        await Task.Delay(2500);

        // todo: dispatch more actions to collect the schema model / attatchments / etc. ??
        
        dispatcher.Dispatch(new FetchRecordsResultAction(records));
    }

    [EffectMethod]
    public async Task HandleFetchRecordAction(FetchRecordAction action, IDispatcher dispatcher)
    {
        // todo: polly resilience, error handling, etc.
        var records = await PaganodApiClient.GetAsync(action.TableName, action.RecordId);

        await Task.Delay(2500);

        dispatcher.Dispatch(new FetchRecordResultAction(records));
    }

    [EffectMethod]
    public async Task HandleSaveCurrentRecordAction(SaveRecordAction action, IDispatcher dispatcher)
    {
        // todo: polly resilience, error handling, etc.
        // var records = await PaganodApiClient.GetAsync(action.TableName, action.RecordId);
        Console.WriteLine(JsonSerializer.Serialize(action.Record));

        await Task.Delay(2500);

        dispatcher.Dispatch(new SaveRecordResultAction(action.Record));
    }
}