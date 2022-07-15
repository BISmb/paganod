using Paganod.Data;
using Paganod.Services;
using Paganod.Worker;

var builder = WebApplication.CreateBuilder(args);

//builder.Logging.AddConsole();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/* Services */
builder.Services.ConfigurePaganodDatabaseLayer(builder.Configuration);
builder.Services.AddScoped<IDataService, DataService>();

builder.Services.ConfigureSwaggerGen(options =>
{
    //.EnableAnnotations();

    //// add JWT Authentication
    //var securityScheme = new OpenApiSecurityScheme
    //{
    //    Name = "JWT Authentication",
    //    Description = "Enter JWT Bearer token **_only_**",
    //    In = ParameterLocation.Header,
    //    Type = SecuritySchemeType.Http,
    //    Scheme = "bearer", // must be lower case
    //    BearerFormat = "JWT",
    //    Reference = new OpenApiReference
    //    {
    //        Id = JwtBearerDefaults.AuthenticationScheme,
    //        Type = ReferenceType.SecurityScheme
    //    }
    //};
    //c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    //c.AddSecurityRequirement(new OpenApiSecurityRequirement
    //{
    //    {securityScheme, Array.Empty<string>() }
    //});
    //c.SwaggerDoc("v1", new OpenApiInfo
    //{
    //    Title = "My API",
    //    Version = "v1",
    //    Description = "My API"
    //});
});

builder.Services.AddHostedService<Worker>();

builder.Services.AddRazorPages(); // add for hosting

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseBlazorFrameworkFiles();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
    endpoints.MapFallbackToFile("index.html");
});

//app.MapControllers();
//app.MapRazorPages();

//app.MapFallbackToFile("index.html");

app.Run();

//record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}
