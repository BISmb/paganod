using Microsoft.AspNetCore.Mvc;
using Paganod.Api.Controllers;
using Paganod.Shared.Constants;
using Paganod.Types.Api.Config.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Paganod.Services.Data.Api.Controllers;

/// <summary>
/// Controller class that holds all data CRUD actions
/// </summary>
//[SwaggerTag("Config")]
[Route(ApiRoutes.Config.Home)]
public partial class ConfigController : PaganodControllerBase
{
    private readonly IConfigService _configService;

    public ConfigController(IConfigService prmConfigService)
    {
        _configService = prmConfigService;
    }

    /// <summary>
    ///     Create a new Schema
    /// </summary>
    [HttpPost(ApiRoutes.Config.New)]
    public async Task<IActionResult> NewSchema(
        [FromBody] CreateSchemaCommand requestModel)
    {
        try
        {
            var response = await _configService.CreateSchemaAsync(requestModel);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    ///     Get a Schema
    /// </summary>
    [HttpPost(ApiRoutes.Config.GetBySchemaId)]
    public async Task<IActionResult> GetSchemaBySchemaId(
        [FromRoute(Name = ApiRoutes.Config.Args.SchemaId)] Guid schemaId)
    {
        try
        {
            var response = await _configService.GetSchemaAsync(schemaId);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    ///     Get a Schema
    /// </summary>
    [HttpPost(ApiRoutes.Config.GetByTableName)]
    public async Task<IActionResult> GetSchemaByTableName(
        [FromRoute(Name = ApiRoutes.Config.Args.TableName)] string tableName)
    {
        try
        {
            var response = await _configService.GetSchemaAsync(tableName);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}