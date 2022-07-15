using System;

namespace Paganod.Web.Store.SchemaUseCase.Actions;

public class FetchConfigSchemaAction
{
    public Guid SchemaId { get; init; }
    public string SchemaTableName { get; init; }

    public FetchConfigSchemaAction(Guid newSchemaId)
    {
        SchemaId = newSchemaId;
    }

    public FetchConfigSchemaAction(string schemaTableName)
    {
        SchemaTableName = schemaTableName;
    }
}