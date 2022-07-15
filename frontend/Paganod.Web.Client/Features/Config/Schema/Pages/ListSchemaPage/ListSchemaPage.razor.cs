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
using Paganod.Web.Client.Store.SchemaUseCase;
using Paganod.Web.Client.Types;
using Paganod.Web.Client.Types.ViewModels;
using Paganod.Web.Features.Config.Schema.Components;
using Paganod.Web.Store.SchemaUseCase;
using Paganod.Web.Store.SchemaUseCase.Actions;

namespace Paganod.Web.Features.Config.Schema.Pages;

public partial class ListSchemaPage : FluxorComponent
{
    [Inject]
    internal IState<ConfigSchemaState> ConfigSchemaState { get; set; }

    [Inject]
    private IDispatcher Dispatcher { get; set; }

    [Inject]
    private IActionSubscriber ActionSubscriber { get; set; }

    [Inject]
    private NavigationManager Nav { get; set; }

    [Inject]
    private ILogger<ListSchemaPage> Logger { get; set; }

    protected override Task OnParametersSetAsync()
    {
        Dispatcher.Dispatch(new FetchSchemasAction());
        return base.OnParametersSetAsync();
    }

    protected void NavigateToSchemaEditPage(Guid schemaModelId)
    {
        string uri = $"config/schema/{schemaModelId}";

        Logger.LogInformation($"Navigating to {uri}");
        Nav.NavigateTo(uri, true);
    }

    protected void NavigateToCreateNewSchemaPage()
    {
        Nav.NavigateTo($"config/schema/new");
    }
}