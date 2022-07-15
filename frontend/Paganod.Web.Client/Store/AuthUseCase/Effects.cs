using System;
using Fluxor;
using Paganod.Web.Client.Store.AuthUseCase.Actions;

namespace Paganod.Web.Client.Store.AppUseCase;

public class Effects
{
    public Effects()
    {

    }

    [EffectMethod]
    public async Task HandleFetchJwtAction(FetchJwtAction action, IDispatcher dispatcher)
    {
        

        // save jwt as local storage cookie

        //dispatcher.Dispatch(new FetchJwtActionResult(jwt));
    }
}

