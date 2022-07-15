using System;

namespace Paganod.Web.Store.AttachmentsUseCase.Actions;

public class FetchAttachmentsAction
{
    public string TableName { get; init; }
    public Guid RecordId { get; init; }

    public FetchAttachmentsAction(string tableName, Guid recordId)
    {
        TableName = tableName;
        RecordId = recordId;
    }
}