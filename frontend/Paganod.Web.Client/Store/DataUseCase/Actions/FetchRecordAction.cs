using System;

namespace Paganod.Web.Store.DataUseCase.Actions;

public class FetchRecordAction
{
    public readonly string TableName;
    public readonly Guid RecordId;

    public FetchRecordAction(string tableName, Guid recordId)
    {
        TableName = tableName;
        RecordId = recordId;
    }
}