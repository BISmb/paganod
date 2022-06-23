using System.Threading.Tasks;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Paganod.Web.Client.Types;
using Paganod.Web.Store;
using Paganod.Web.Store.DataUseCase;
using Paganod.Web.Store.DataUseCase.Actions;
using Paganod.Web.Store.SchemaUseCase;
using Paganod.Web.Store.SchemaUseCase.Actions;

namespace Paganod.Web.Features.Data.Pages;

public partial class ViewRecordsPage : FluxorComponent
{
    [Parameter]
    public string TableName { get; set; }

    [Inject]
    internal IState<DataState> DataState { get; set; }

    [Inject]
    internal IState<ConfigSchemaState> SchemaState { get; set; }

    [Inject]
    private IDispatcher Dispatcher { get; set; }

    [Inject]
    private IActionSubscriber ActionSubscriber { get; set; }

    [Inject]
    private NavigationManager Nav { get; set; }

    protected override Task OnInitializedAsync()
    {
        if (SchemaState.Value.SchemaModel is null || SchemaState.Value.SchemaModel.TableName != TableName)
            Dispatcher.Dispatch(new FetchConfigSchemaAction(TableName));

        if (DataState.Value.CurrentRecords is null || DataState.Value.CurrentRecords.Count == 0)
            Dispatcher.Dispatch(new FetchRecordsAction(TableName));

        return base.OnInitializedAsync();
    }

    private void NavigateToRecord(Record record)
    {
        var pk = record[SchemaState.Value.SchemaModel.PrimaryKeyName]; // todo: 

        ActionSubscriber.SubscribeToAction<SetCurrentRecordAction>(this, actionResult => Nav.NavigateTo($"/data/{actionResult.TableName}/{actionResult.RecordId}"));
        Dispatcher.Dispatch(new FetchConfigSchemaAction(TableName));
        Dispatcher.Dispatch(new SetCurrentRecordAction(TableName, pk));

        //ActionSubscriber.SubscribeToAction<FetchConfigSchemaResultAction>(this, _ => Nav.NavigateTo($"/data/{TableName}/{pk}"));
    }
}