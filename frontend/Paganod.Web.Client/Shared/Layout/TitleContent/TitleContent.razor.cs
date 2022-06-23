using Microsoft.AspNetCore.Components;

namespace Paganod.Web.Client.Shared.Layout;

public partial class TitleContent : ComponentBase
{
    [Parameter]
    public string TitleText { get; set; }

    [Parameter] 
    public RenderFragment ChildContent { get; set; }
}