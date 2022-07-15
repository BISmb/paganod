using System;
using Microsoft.AspNetCore.Mvc;
using OneOf;
using Paganod.Services;
using Paganod.Shared.Constants;
using Paganod.Shared.Types;

namespace Paganod.Api.Controllers;

public abstract class PaganodApiController
{
    private readonly ILogger _logger;

    protected PaganodApiController(ILogger prmLogger)
    {
        _logger = prmLogger;
    }

    protected virtual IActionResult NotHandled()
    {
        return new BadRequestResult();
    }

    protected virtual IActionResult Bad(object? value)
    {
         return value is null
            ? new BadRequestResult()
            : new BadRequestObjectResult(value);
    }

    protected virtual IActionResult Ok(object value)
    {
        return new OkObjectResult(value);
    }
}

[Route(ApiRoutes.Data.Home)]
public class DataController : PaganodApiController
{
    private readonly IDataService _dataService;

    public DataController(ILogger<DataController> prmLogger, IDataService prmDataService)
        : base(prmLogger)
    {
        _dataService = prmDataService;
    }

    /// <summary>
    /// Get many records by table name
    /// </summary>
    /// <param name="tableName">Table Name</param>
    /// <param name="pageNumber">Page Number</param>
    /// <param name="resultsPerPage">Results per Page</param>
    /// <returns>Many records from @tableName</returns>
    [HttpGet(ApiRoutes.Data.GetMany)]
    public async Task<IActionResult> GetManyRecordsAsync(
        [FromRoute(Name = ApiRoutes.Data.Args.TableName)] string tableName,
        [FromRoute(Name = ApiRoutes.Data.Args.PageNumber)] int pageNumber,
        [FromRoute(Name = ApiRoutes.Data.Args.ResultsPerPage)] int resultsPerPage)
    {
        var result = await _dataService.GetRecordsAsync(tableName, pageNumber, resultsPerPage);

        return result.Value switch
        {
            IEnumerable<Record> => Ok(result.Value),

            _ => NotHandled(),
        };
    }

    /// <summary>
    /// Get a specific record by id
    /// </summary>
    /// <param name="tableName">Table Name</param>
    /// <param name="recordId">Record Id</param>
    /// <returns>A single record from @tableName matches the provided @recordId</returns>
    [HttpGet(ApiRoutes.Data.Get)]
    public async Task<IActionResult> GetRecordAsync(
        [FromRoute(Name = ApiRoutes.Data.Args.TableName)] string tableName,
        [FromRoute(Name = ApiRoutes.Data.Args.Id)] Guid recordId)
    {
        var result = await _dataService.GetRecordAsync(tableName, recordId);

        return result.Value switch
        {
            Record => Ok(result.Value),

            _ => NotHandled(),
        };
    }
}

