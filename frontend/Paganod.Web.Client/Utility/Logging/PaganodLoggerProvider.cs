using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging.Configuration;

namespace Paganod.Web.Client.Utility.Logging;

public sealed class PaganodLoggerProvider : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName)
    {
        return new PaganodLogger(categoryName, () => new CustomLoggerConfiguration());
    }

    public void Dispose()
    {
        //throw new NotImplementedException();
    }
}

public static class CustomLoggerExtensions
{
    public static ILoggingBuilder AddCustomLogger(
        this ILoggingBuilder builder)
    {
        builder.AddConfiguration();

        builder.Services.TryAddEnumerable(
            ServiceDescriptor.Singleton<ILoggerProvider, PaganodLoggerProvider>());

        LoggerProviderOptions.RegisterProviderOptions
            <CustomLoggerConfiguration, PaganodLoggerProvider>(builder.Services);

        return builder;
    }

    public static ILoggingBuilder AddCustomJsLogger(
        this ILoggingBuilder builder)
    {
        builder.AddConfiguration();

        builder.Services.TryAddEnumerable(
            ServiceDescriptor.Singleton<ILoggerProvider, PaganodJsLoggerProvider>());

        LoggerProviderOptions.RegisterProviderOptions
            <CustomLoggerConfiguration, PaganodJsLoggerProvider>(builder.Services);

        return builder;
    }
}
