using Paganod.Web.Client.Types;

namespace Paganod.Web.Store.DataUseCase.Actions;

public class FetchRecordsResultAction
{
    public readonly IEnumerable<Record> Records;

    public FetchRecordsResultAction(IEnumerable<Record> records)
    {
        Records = records;
    }
}