using System;
namespace Paganod.Web.Client.Store.AuthUseCase.Actions;

public class FetchJwtActionResult
{
    public readonly string? Jwt;

    public FetchJwtActionResult(string jwt)
    {
        Jwt = jwt;
    }
}

