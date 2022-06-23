using System.Data;
using Paganod.Api.Shared.Feature.Config.Schema.Commands;
using Paganod.Web.Client.Types;

namespace Paganod.Web.Store.SchemaUseCase.Actions;

public class CreateSchemaAction
{    public readonly CreateSchemaCommand Model;

    public CreateSchemaAction(CreateSchemaCommand command)
    {
        Model = command;
    }
}