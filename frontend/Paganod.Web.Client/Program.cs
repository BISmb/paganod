using System;
using System.Collections.Concurrent;
using Blazored.Modal;
using Fluxor;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;
using Paganod.Api.Client;
using Paganod.Api.Shared;
using Paganod.Web.Client;
using Paganod.Web.Client.Utility.Logging;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Logging.ClearProviders().AddCustomLogger();

builder.Services.AddBlazoredModal();
builder.Services.AddFluxor(options => options.ScanAssemblies(typeof(Program).Assembly));
builder.Services.AddHttpClient("Paganod", options => options.BaseAddress = new Uri("http://localhost:5000"));

if (builder.HostEnvironment.IsDevelopment())

    builder.Services.AddScoped<IPaganodApiClient>(services =>
    {
#if DEBUG
    return new MockedApiClient();
#else
        string apiUrl = "http://localhost:5000";
        return new PaganodApiClient(apiUrl);
#endif
    });

await builder.Build().RunAsync();