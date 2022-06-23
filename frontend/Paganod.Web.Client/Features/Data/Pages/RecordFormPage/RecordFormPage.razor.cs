using System;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Paganod.Web.Store.AttachmentsUseCase;
using Paganod.Web.Store.AttachmentsUseCase.Actions;
using Paganod.Web.Store.DataUseCase;
using Paganod.Web.Store.DataUseCase.Actions;
using Paganod.Web.Store.SchemaUseCase;
using Paganod.Web.Store.SchemaUseCase.Actions;

namespace Paganod.Web.Features.Data.Pages;

public partial class RecordFormPage : FluxorComponent
{
    [Parameter]
    public string TableName { get; set; }

    [Parameter]
    public Guid RecordId { get; set; }

    [Inject]
    private IState<ConfigSchemaState> SchemaState { get; set; }

    [Inject]
    internal IState<DataState> DataState { get; set; }

    [Inject]
    internal IState<AttachmentsState> AttachmentsState { get; set; }

    [Inject]
    private IDispatcher Dispatcher { get; set; }

    [Inject]
    private IActionSubscriber ActionSubscriber { get; set; }

    protected override Task OnParametersSetAsync()
    {
        if (SchemaState.Value.SchemaModel is null || SchemaState.Value.SchemaModel.TableName != TableName)
            Dispatcher.Dispatch(new FetchConfigSchemaAction(TableName));
        
        // should always get the latest record from the server
        Dispatcher.Dispatch(new FetchRecordAction(TableName, RecordId));
        Dispatcher.Dispatch(new FetchAttachmentsAction(TableName, RecordId));

        return base.OnParametersSetAsync();
    }
    
    private void SaveRecord()
    {
        Dispatcher.Dispatch(new SaveRecordAction(DataState.Value.CurrentRecord));
    }
}