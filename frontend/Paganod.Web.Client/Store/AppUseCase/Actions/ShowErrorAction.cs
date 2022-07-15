using System;
namespace Paganod.Web.Client.Store.AppUseCase.Actions;

public class ShowErrorAction
{
    public Exception? Exception { get; init; }
    public string? Message { get; init; }

    public ShowErrorAction(Exception prmException)
    {
        Exception = prmException;
    }

    public ShowErrorAction(string message)
    {
        Message = message;
    }
}

