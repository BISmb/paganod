using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.Modal;
using Blazored.Modal.Services;

using Fluxor;
using Fluxor.Blazor.Web.Components;

using Microsoft.AspNetCore.Components;
using Paganod.Web.Client.Features.Config.Schema;
using Paganod.Web.Client.Features.Config.Schema.Components;
using Paganod.Web.Client.Types;
using Paganod.Web.Client.Types.ViewModels;
using Paganod.Web.Features.Config.Schema.Components;
using Paganod.Web.Store.SchemaUseCase;
using Paganod.Web.Store.SchemaUseCase.Actions;

namespace Paganod.Web.Features.Config.Schema.Pages;

public partial class EditSchemaPage : FluxorComponent
{
    [Parameter]
    public string TableName { get; set; }

    [Parameter]
    public Guid SchemaId { get; set; }

    [Inject]
    internal IState<ConfigSchemaState> ConfigSchemaState { get; set; }

    [Inject]
    private IDispatcher Dispatcher { get; set; }

    [Inject]
    private IActionSubscriber ActionSubscriber { get; set; }

    [Inject]
    private NavigationManager Nav { get; set; }

    [CascadingParameter] 
    public IModalService Modal { get; set; }

    private bool ChangesDetected = false;
    protected EditSchemaFormViewModel FormModel;
    
    protected override Task OnParametersSetAsync()
    {
        if (SchemaId == Guid.Empty && string.IsNullOrWhiteSpace(TableName))
            Modal.Show<NewSchemaModal>($"Create a new Schema");

        ActionSubscriber.SubscribeToAction<SaveConfigSchemaResultAction>(this, HandleSchemaSaveResultAction);

        if (SchemaId != Guid.Empty)
            Dispatcher.Dispatch(new FetchConfigSchemaAction(SchemaId));
        else if (!String.IsNullOrWhiteSpace(TableName))
            Dispatcher.Dispatch(new FetchConfigSchemaAction(TableName));           

        return base.OnParametersSetAsync();
    }

    protected void OnFormSubmit()
    {
        Dispatcher.Dispatch(new SaveConfigSchemaAction(FormModel));
    }

    private void HandleSchemaSaveResultAction(SaveConfigSchemaResultAction action)
    {
        // open Modal for IMigrationState<>?
        var parameters = new ModalParameters();
        parameters.Add(nameof(ScheduleMigrationModal.ForwardMigrationId), action.ForwardMigrationId);
        parameters.Add(nameof(ScheduleMigrationModal.SunsetMigrationId), action.SunsetMigrationId);

        Modal.Show<ScheduleMigrationModal>("Schedule Migration", parameters);

        // Provide both Migration Id's as input

        // When "Submit", post both migrations to Database with schedule date


        // example:
        // Console.WriteLine($"Are migrations needed? {action.MigrationsNeeded}");

        // if SchemaConfigSaveResult has migrations that are "true"
        // then need to show window pop-up / modal
        // then dispatch an action to set the migrations that are scheduled
    }

    private void LoadSchemaModelForEdit(FetchConfigSchemaResultAction action)
    {
        if (action.SchemaModel is not null)
        {
            FormModel = new EditSchemaFormViewModel()
            {
                SolutionId = action.SchemaModel.SolutionId,
                TableName = action.SchemaModel.TableName,
                PrimaryKeyName = action.SchemaModel.PrimaryKeyName,
                PrimaryKeyType = action.SchemaModel.PrimaryKeyType,

                Columns = new ObservableCollection<EditSchemaColumnViewModel>(
                    action.SchemaModel.Columns.Select(x => new EditSchemaColumnViewModel(x.Name, x.Type, x.ColumnId, x.Options))
                ),
            };
            FormModel.StartTracking();
        }

        StateHasChanged();
    }

    private void FormStateHasChanged()
    {
        Console.WriteLine("Form State Change Detected");
        ChangesDetected = true;
        StateHasChanged();
    }
}