using Fluxor;
using Paganod.Api.Client;
using Paganod.Api.Shared;
using Paganod.Web.Client.Store.AppUseCase.Actions;
using Paganod.Web.Client.Types;
using Paganod.Web.Store.AttachmentsUseCase.Actions;

namespace Paganod.Web.Store.AttachmentsUseCase;

public class Effects
{
    private readonly ILogger<Effects> _logger;
    private readonly IPaganodApiClient PaganodApiClient;

    public Effects(IPaganodApiClient paganodApiClient)
    {
        PaganodApiClient = paganodApiClient;
    }

    [EffectMethod]
    public async Task HandleFetchAttachmentsAction(FetchAttachmentsAction action, IDispatcher dispatcher)
    {
        try
        {

        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new ShowErrorAction(ex));
        }
        // todo: polly resilience, error handling, etc.
        //var attachments = await PaganodApiClient.GetAttachmentsAsync(action.TableName, action.RecordId);

        await Task.Delay(2500);

        // todo: dispatch more actions to collect the schema model / attatchments / etc. ??

        var attchments = Enumerable.Empty<Attachment>();
        
        dispatcher.Dispatch(new FetchAttachmentsResultAction(attchments));
    }
}