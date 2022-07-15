using System;
using Microsoft.JSInterop;

namespace Paganod.Web.Client.Utility.Logging;

public sealed class PaganodJsLoggerProvider : ILoggerProvider
{
    private readonly IJSRuntime _jsRuntime;
    public PaganodJsLoggerProvider(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new PaganodJsLogger(categoryName, () => new CustomLoggerConfiguration(), _jsRuntime);
    }

    public void Dispose()
    {
        //throw new NotImplementedException();
    }
}

