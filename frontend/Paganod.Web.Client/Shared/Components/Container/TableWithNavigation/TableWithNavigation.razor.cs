using Fluxor;
using Microsoft.AspNetCore.Components;
using Paganod.Api.Client;
using Paganod.Api.Shared;
using Paganod.Api.Shared.Feature.Config.Schema.Responses;
using Paganod.Web.Store.SchemaUseCase;

namespace Paganod.Web.Client.Shared.Components.Container;

public partial class TableWithNavigation : ComponentBase
{
    [Inject]
    private IPaganodApiClient Api { get; set; }

    Func<SchemaModelDto> GetSchemaModelDtos;

    [Inject]
    private IState<ConfigSchemaState> SchemaState { get; set; }

    protected override Task OnAfterRenderAsync(bool firstRender)
    {



        return base.OnAfterRenderAsync(firstRender);
    }
}