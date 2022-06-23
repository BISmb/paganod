namespace Paganod.Web.Store.DataUseCase.Actions;

public class SetCurrentRecordAction
{
    public readonly string TableName;
    public readonly object RecordId;

    public SetCurrentRecordAction(string tableName,
                                    object recordId)
    {
        TableName = tableName;
        RecordId = recordId;
    }
}