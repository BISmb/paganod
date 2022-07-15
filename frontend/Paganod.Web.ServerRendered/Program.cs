using Blazored.Modal;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Paganod.Api;
using Paganod.Api.Client;
using Paganod.Web.Client.Utility.Logging;
using Paganod.Web.ServerRendered.Data;

var webAppOptions = new WebApplicationOptions()
{
    Args = args,
    ContentRootPath = "/Users/marty/Documents/GitHub/paganod/frontend/Paganod.Web.Client/wwwroot",
    WebRootPath = "/Users/marty/Documents/GitHub/paganod/frontend/Paganod.Web.Client/wwwroot",
};

var builder = WebApplication.CreateBuilder(webAppOptions);

//builder.Environment.ContentRootPath = "/Users/marty/Documents/GitHub/paganod/frontend/Paganod.Web.Client/wwwroot";

//builder.Services.AddSingleton<IPaganodLogger, PaganodLogger>();
builder.Services.AddBlazoredModal();
builder.Services.AddFluxor(options => options.ScanAssemblies(typeof(Program).Assembly));
builder.Services.AddHttpClient("Paganod", options => options.BaseAddress = new Uri("http://localhost:5000"));

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

//builder.Services.AddCoreServices(builder.Configuration);
//builder.Services.AddConfigServices(builder.Configuration);
//builder.Services.AddDataServices(builder.Configuration);

//builder.Services.AddScoped<IPaganodApiClient, PaganodServerSideClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

