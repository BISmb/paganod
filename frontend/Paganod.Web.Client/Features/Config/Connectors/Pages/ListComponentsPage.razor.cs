using System;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Paganod.Web.Client.Store.ConnectorUseCase;
using Paganod.Web.Client.Store.ConnectorUseCase.Actions;
using Paganod.Web.Store.SchemaUseCase;

namespace Paganod.Web.Client.Features.Config.Connectors.Pages;

public partial class ListComponentsPage : FluxorComponent
{
    [Inject]
    internal IState<ConnectorState> ConnectorState { get; set; }

    [Inject]
    private IDispatcher Dispatcher { get; set; }

    [Inject]
    private IActionSubscriber ActionSubscriber { get; set; }

    [Inject]
    private NavigationManager Nav { get; set; }

    protected override Task OnParametersSetAsync()
    {
        Dispatcher.Dispatch(new FetchConnectorsAction());
        return base.OnParametersSetAsync();
    }
}

