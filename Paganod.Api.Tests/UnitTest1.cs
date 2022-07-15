using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Paganod.Api.Controllers;
using Paganod.Services;
using Paganod.Types.Api.Config.Commands;

namespace Paganod.Api.Tests;

[TestClass]
public class UnitTest1
{
    private static T GetObjectResultContent<T>(ActionResult<T> result)
    {
        return (T)((ObjectResult)result.Result).Value;
    }

    [TestMethod]
    public async Task Api_Controller_Returns_BadRequest()
    {
        // setup
        var configServiceMock = new Mock<IConfigService>();
        string tableName = "transactions";

        //configServiceMock.Setup(x => x.CreateSchemaAsync(It.IsAny<CreateSchemaCommand>(), It.IsAny<CancellationToken>()))
        //                 .Returns(Task.FromResult(new ErrorResponse($"{tableName} already exists")));

        //var apiController = new DataController(configServiceMock.Object);

        //// act
        //var request = new CreateSchemaCommand()
        //{
        //    TableName = tableName,
        //};

        //var response = await apiController.NewSchema(default);

        // assert
        //response.Should().BeOfType<NotFoundResult>();
        //response.Should().Equals(new BadRequestObjectResult($"{tableName} already exists"));
        //var okResult = response as OkObjectResult;

    }
}
