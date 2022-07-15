using System;
using Microsoft.JSInterop;

namespace Paganod.Web.Client.Utility.Logging;

public sealed class PaganodJsLogger : PaganodLogger, ILogger
{
    private readonly IJSRuntime _jsRuntime;

    public PaganodJsLogger(string categoryName, Func<CustomLoggerConfiguration> getCurrentConfig, IJSRuntime jsRuntime)
        : base(categoryName, getCurrentConfig)
    {
        _jsRuntime = jsRuntime;
    }

    protected override void LogMessage(LogLevel logLevel, string message)
    {
        string jsConsole = logLevel switch
        {
            LogLevel.Error => "console.error",
            LogLevel.Debug => "console.debug",
            LogLevel.Warning => "console.warn",
            LogLevel.Information => "console.info",

            _ => "console.log",
        };

        // logging is not a critical function and exceptions do not have to be handled
        var jsLog = _jsRuntime.InvokeVoidAsync(jsConsole, message);
        jsLog.AsTask().Start();
    }
}