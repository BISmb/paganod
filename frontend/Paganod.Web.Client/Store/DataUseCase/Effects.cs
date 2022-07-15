using System.Text.Json;
using Fluxor;
using Paganod.Api.Client;
using Paganod.Api.Shared;
using Paganod.Shared.Types;
using Paganod.Web.Client.Store.AppUseCase.Actions;
using Paganod.Web.Client.Store.DataUseCase.Actions;
using Paganod.Web.Store.DataUseCase.Actions;

namespace Paganod.Web.Store.DataUseCase;

public class Effects
{
    private readonly ILogger _logger;
    private readonly IPaganodApiClient PaganodApiClient;

    public Effects(IPaganodApiClient paganodApiClient, ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger("Data::Effects");
        PaganodApiClient = paganodApiClient;
    }

    //[EffectMethod]
    //public async Task HandleFetchRecordsAction(FetchRecordsAction action, IDispatcher dispatcher)
    //{
    //    try
    //    {
    //        // todo: polly resilience, error handling, etc.
    //        var records = await PaganodApiClient.GetAsync(action.TableName, 1, 15);
            
    //        // todo: dispatch more actions to collect the schema model / attatchments / etc. ??

    //        dispatcher.Dispatch(new FetchRecordsResultAction(records));
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine("Dispatching error occurred");
    //        dispatcher.Dispatch(new ShowErrorAction(ex));
    //    }
    //}

    //[EffectMethod]
    //public async Task HandleFetchRecordAction(FetchRecordAction action, IDispatcher dispatcher)
    //{
    //    // todo: polly resilience, error handling, etc.
    //    var record = await PaganodApiClient.GetAsync(action.TableName, action.RecordId);

    //    await Task.Delay(2500);

    //    dispatcher.Dispatch(new FetchRecordResultAction(record));
    //}

    //[EffectMethod]
    //public async Task HandleSaveCurrentRecordAction(SaveRecordAction action, IDispatcher dispatcher)
    //{
    //    // todo: polly resilience, error handling, etc.
    //    // var records = await PaganodApiClient.GetAsync(action.TableName, action.RecordId);
    //    Console.WriteLine(JsonSerializer.Serialize(action.Record));

    //    await Task.Delay(2500);

    //    dispatcher.Dispatch(new SaveRecordResultAction(action.Record));
    //}
}