using System.Collections.ObjectModel;
using Fluxor;
using Paganod.Web.Client.Store.AppUseCase.Actions;
using Paganod.Web.Client.Types;
using Paganod.Web.Client.Types.ViewModels;
using Paganod.Web.Store.SchemaUseCase.Actions;

namespace Paganod.Web.Store.AppUseCase;

public static class Reducers
{
    [ReducerMethod]
    public static LoadingState ReduceLoadingAction(LoadingState state, LoadingAction action)
    {
        return state with { IsLoading = true, StatusMessage = action.Message, DisableUserInput = true };
    }

    [ReducerMethod]
    public static LoadingState ReduceChangeLoadingStatusMessageAction(LoadingState state, ChangeLoadingStatusMessageAction action)
    {
        return state with { StatusMessage =  action.NewStatusMessage };
    }

    [ReducerMethod]
    public static LoadingState ReduceLoadingCompleteAction(LoadingState state, LoadingCompleteAction action)
    {
        return state with { IsLoading = false, DisableUserInput = false, StatusMessage =  "" };
    }
}