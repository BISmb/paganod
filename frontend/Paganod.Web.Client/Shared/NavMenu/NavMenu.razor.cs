using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Paganod.Web.Store.MetadataUseCase;
using Paganod.Web.Store.MetadataUseCase.Actions;
using Paganod.Web.Store.SchemaUseCase;

namespace Paganod.Web.Client.Shared;

public partial class NavMenu : FluxorComponent
{
    [Inject]
    internal IState<MetadataState> MetadataState { get; set; }

    [Inject]
    internal IState<ConfigSchemaState> SchemaState { get; set; }

    [Inject]
    private IDispatcher Dispatcher { get; set; }
    
    private bool collapseNavMenu = false;

    private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

    protected override Task OnParametersSetAsync()
    {
        if (MetadataState.Value.SchemaModelsMetadata is null)
            Dispatcher.Dispatch(new RefreshMetadataAction());

        // Dispatcher.Dispatch(new FetchSchemaListAction());

        return base.OnParametersSetAsync();
    }

    private void ToggleNavMenu()
    {
        collapseNavMenu = !collapseNavMenu;
    }
}