using System;
using System.Threading.Tasks;
using Blazored.Modal;
using Blazored.Modal.Services;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;

using Paganod.Web.Client.Types;
using Paganod.Web.Store.DataUseCase;
using Paganod.Web.Store.DataUseCase.Actions;

namespace Paganod.Web.Features.Data.Components;

public partial class LookupModal : FluxorComponent
{
    [Parameter]
    public string TableName { get; set; } // eventually change to ViewId name

    [CascadingParameter] 
    public BlazoredModalInstance ModalInstance { get; set; }

    [Inject]
    internal IState<DataState> DataState { get; set; }

    // [Inject]
    // internal IState<ConfigSchemaState> SchemaState { get; set; }

    [Inject]
    private IDispatcher Dispatcher { get; set; }

    [Inject]
    private IActionSubscriber ActionSubscriber { get; set; }

    protected Record[] AvailableLookupRecords;

    protected override Task OnParametersSetAsync()
    {
        ActionSubscriber.SubscribeToAction<FetchRecordsResultAction>(this, SetAccountLocalRecordsVariable);
        Dispatcher.Dispatch(new FetchRecordsAction(TableName));

        return base.OnParametersSetAsync();
    }

    private void SetAccountLocalRecordsVariable(FetchRecordsResultAction resultAction)
    {
        AvailableLookupRecords = new Record[0];

        if (resultAction.Records.Any(x => x.TableName != TableName))
            AvailableLookupRecords = resultAction.Records.ToArray();
        
        StateHasChanged();
    }

    private void SelectRecord(Guid recordId)
    {
        ModalInstance.CloseAsync(ModalResult.Ok(recordId));
    }
}