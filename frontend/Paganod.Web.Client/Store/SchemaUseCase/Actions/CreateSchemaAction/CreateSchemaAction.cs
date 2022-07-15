using Paganod.Types.Api.Config.Commands;

namespace Paganod.Web.Store.SchemaUseCase.Actions;

public class CreateSchemaAction
{    public readonly CreateSchemaCommand Model;

    public CreateSchemaAction(CreateSchemaCommand command)
    {
        Model = command;
    }
}