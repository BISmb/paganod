namespace Paganod.Web.Client.Store.AppUseCase.Actions;

public class ChangeLoadingStatusMessageAction
{
    public readonly string NewStatusMessage;

    public ChangeLoadingStatusMessageAction(string newStatusMessage)
    {
        NewStatusMessage = newStatusMessage;
    }
}