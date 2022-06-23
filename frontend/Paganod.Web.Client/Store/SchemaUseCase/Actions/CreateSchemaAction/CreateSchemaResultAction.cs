namespace Paganod.Web.Store.SchemaUseCase.Actions;

public class CreateSchemaResultAction
{
    public readonly Guid SchemaId;

    public CreateSchemaResultAction(Guid? newSchemaId)
    {
        SchemaId = newSchemaId.HasValue ? newSchemaId.Value : Guid.Empty;
    }
}