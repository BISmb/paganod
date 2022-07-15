using System;
using Microsoft.AspNetCore.Mvc;

namespace Paganod.Api.Controllers;

public abstract class PaganodControllerBase : ControllerBase
{
    public PaganodControllerBase()
    {

    }

    protected IActionResult HandleError(Exception ex)
    {
#if !DEBUG
        return StatusCode(500);
#else
        return StatusCode(500, ex);
#endif
    }
}

