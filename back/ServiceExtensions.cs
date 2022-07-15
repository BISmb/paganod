using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Paganod.Api.Services.Config;
using Paganod.Api.Services.Data;
using Paganod.Data;
using Paganod.Data.Shared.Interfaces;
using System.Reflection;

namespace Paganod.Api;

public static class ServiceExtensions
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigurePaganodDatabaseLayer(configuration);

        return services;
    }

    public static IServiceCollection AddConfigServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add Data API Controllers
        services.AddControllers()
            .AddApplicationPart(Assembly.GetExecutingAssembly());

        services.AddScoped<IConfigService>(services => new ConfigService(services.GetRequiredService<IAppDbContext>()));

        return services;
    }

    public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add Data API Controllers
        services.AddControllers()
            .AddApplicationPart(Assembly.GetExecutingAssembly());

        services.AddScoped<IDataService, DataService>();

        return services;
    }
}
