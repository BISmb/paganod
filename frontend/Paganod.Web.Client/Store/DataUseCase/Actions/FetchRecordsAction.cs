namespace Paganod.Web.Store.DataUseCase.Actions;

public class FetchRecordsAction
{
    public string TableName { get; }
    // todo: query parameters

    public FetchRecordsAction(string tableName)
    {
        TableName = tableName;
    }
}