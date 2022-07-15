using System;
using System.Text.Json;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Paganod.Web.Client.Store.AppUseCase.Actions;

namespace Paganod.Web.Client;

public partial class App : FluxorComponent
{
    [Inject]
    private IDispatcher Dispatcher { get; set; }

    [Inject]
    private IActionSubscriber ActionSubscriber { get; set; }

    [Inject]
    private ILogger<App>? Logger { get; set; }

    protected override Task OnParametersSetAsync()
    {
        Dispatcher.ActionDispatched += Dispatcher_ActionDispatched; ;

        return base.OnParametersSetAsync();
    }

    private static bool IsDebug()
    {
#if DEBUG
        return true;
#else
        return false;
#endif
    }

    private void Dispatcher_ActionDispatched(object? sender, ActionDispatchedEventArgs e)
    {
        Logger?.LogInformation($"Dispatched Action: {e.Action.GetType().Name}");
        Logger?.LogInformation(Json(e.Action));
    }

    public string Json(object value)
    {
        var jsonSerializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
        };
        return JsonSerializer.Serialize(value, value.GetType(), jsonSerializerOptions);
    }
}