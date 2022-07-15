using System;

namespace Paganod.Web.Store.DataUseCase.Actions;

public class FetchRecordAction
{
    public string TableName { get; init; }
    public Guid RecordId { get; init; }

    public FetchRecordAction(string tableName, Guid recordId)
    {
        TableName = tableName;
        RecordId = recordId;
    }
}