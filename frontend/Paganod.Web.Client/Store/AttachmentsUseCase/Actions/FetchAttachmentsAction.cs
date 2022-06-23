using System;

namespace Paganod.Web.Store.AttachmentsUseCase.Actions;

public class FetchAttachmentsAction
{
    public readonly string TableName;
    public readonly Guid RecordId;

    public FetchAttachmentsAction(string tableName, Guid recordId)
    {
        TableName = tableName;
        RecordId = recordId;
    }
}