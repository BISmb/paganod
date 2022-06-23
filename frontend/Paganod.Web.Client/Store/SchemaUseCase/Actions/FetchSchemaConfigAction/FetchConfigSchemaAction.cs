using System;

namespace Paganod.Web.Store.SchemaUseCase.Actions;

public class FetchConfigSchemaAction
{
    public readonly Guid SchemaId;
    public readonly string? SchemaTableName;

    public FetchConfigSchemaAction(Guid newSchemaId)
    {
        SchemaId = newSchemaId;
    }

    public FetchConfigSchemaAction(string schemaTableName)
    {
        SchemaTableName = schemaTableName;
    }
}