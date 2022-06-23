using Fluxor;

using Paganod.Web.Client.Services;
using Paganod.Web.Store.AttachmentsUseCase.Actions;

namespace Paganod.Web.Store.AttachmentsUseCase;

public class Effects
{
    private readonly IPaganodApiClient PaganodApiClient;

    public Effects(IPaganodApiClient paganodApiClient)
    {
        PaganodApiClient = paganodApiClient;
    }

    [EffectMethod]
    public async Task HandleFetchAttachmentsAction(FetchAttachmentsAction action, IDispatcher dispatcher)
    {
        // todo: polly resilience, error handling, etc.
        var attachments = await PaganodApiClient.GetAttachmentsAsync(action.TableName, action.RecordId);

        await Task.Delay(2500);

        // todo: dispatch more actions to collect the schema model / attatchments / etc. ??
        
        dispatcher.Dispatch(new FetchAttachmentsResultAction(attachments));
    }
}