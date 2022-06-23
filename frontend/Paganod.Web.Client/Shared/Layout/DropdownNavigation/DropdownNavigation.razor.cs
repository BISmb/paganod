using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;

namespace Paganod.Web.Client.Shared.Layout;

public partial class DropdownNavigation : FluxorComponent
{
    [Parameter]
    public string[] NavigationLinks { get; set; }

    private bool DropdownExpanded;
}