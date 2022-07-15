using System;
using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using static Paganod.Web.Client.Utility.Logging.CustomLoggerConfiguration;

namespace Paganod.Web.Client.Utility.Logging;


// https://docs.microsoft.com/en-us/aspnet/core/blazor/fundamentals/logging?view=aspnetcore-6.0
public class PaganodLogger : ILogger
{
    private readonly string _categoryName;
    private readonly Func<CustomLoggerConfiguration> _getCurrentConfig;

    public PaganodLogger(string categoryName, Func<CustomLoggerConfiguration> getCurrentConfig)
    {
        _categoryName = categoryName;
        _getCurrentConfig = getCurrentConfig;
    }

    public IDisposable BeginScope<TState>(TState state) => default!;

    public bool IsEnabled(LogLevel logLevel) =>
        _getCurrentConfig().LogLevels.ContainsKey(logLevel);

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        CustomLoggerConfiguration config = _getCurrentConfig();

        if (config.EventId == 0 || config.EventId == eventId.Id)
        {
            var stringLogMessage = config.LogLevels[logLevel] switch
            {
                LogFormat.Short => $"{_categoryName}: {formatter(state, exception)}",
                LogFormat.Long => $"[{eventId.Id,2}: {logLevel,-12}] {_categoryName} - {formatter(state, exception)}",

                _ => throw new NotImplementedException($"Log Format: {config.LogLevels[logLevel]} is not implemented"),
            };

            LogMessage(logLevel, stringLogMessage);
        }
    }

    protected virtual void LogMessage(LogLevel logLevel, string message)
    {
        Console.WriteLine(message);
    }
}

//await JSRuntime.InvokeVoidAsync("console.log", "Hello Blazor!");