using System;
using System.Threading.Tasks;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Paganod.Api.Shared.Feature.Config.Schema.Commands;
using Paganod.Web.Client.Store.AppUseCase.Actions;
using Paganod.Web.Store.SchemaUseCase;
using Paganod.Web.Store.SchemaUseCase.Actions;

namespace Paganod.Web.Client.Features.Config.Schema.Components;

public partial class NewSchemaModal : FluxorComponent
{
    [Inject]
    protected IState<ConfigSchemaState> ConfigSchemaState { get; set; }

    [Inject]
    private IDispatcher Dispatcher { get; set; }

    [Inject]
    private IActionSubscriber ActionSubscriber { get; set; }

    [Inject]
    private NavigationManager Nav { get; set; }

    protected override Task OnInitializedAsync()
    {
        ActionSubscriber.SubscribeToAction<CreateSchemaResultAction>(this, NavigateToEditSchemaPage);

        // debug
        ConfigSchemaState.Value.Form.TableName = "transactions";
        ConfigSchemaState.Value.Form.PrimaryKeyName = "transaction_id";
        ConfigSchemaState.Value.Form.PrimaryKeyType = System.Data.DbType.Guid;

        return base.OnInitializedAsync();
    }

    private void NavigateToEditSchemaPage(CreateSchemaResultAction successAction)
    {
        Dispatcher.Dispatch(new LoadingCompleteAction());

        Console.WriteLine(successAction.SchemaId);
        if (successAction.SchemaId != Guid.Empty)
            Nav.NavigateTo($"config/schema/{successAction.SchemaId}");
    }

    private void OnFormSubmit()
    {
        var createSchemaCommand = new CreateSchemaCommand
        {
            TableName = ConfigSchemaState.Value.Form.TableName,
            PrimaryKeyName = ConfigSchemaState.Value.Form.PrimaryKeyName,
            PrimaryKeyType = ConfigSchemaState.Value.Form.PrimaryKeyType,
        };

        Dispatcher.Dispatch(new LoadingAction("Creating Schema..."));
        Dispatcher.Dispatch(new CreateSchemaAction(createSchemaCommand));
    }
}