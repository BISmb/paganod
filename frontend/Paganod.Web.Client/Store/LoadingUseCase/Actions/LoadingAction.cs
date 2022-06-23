namespace Paganod.Web.Client.Store.AppUseCase.Actions;

public class LoadingAction
{
    public readonly string Message;

    public LoadingAction(string statusMesage)
    {
        Message = statusMesage;
    }
}