using Paganod.Web.Client.Types;

namespace Paganod.Web.Store.DataUseCase.Actions;

public class FetchRecordResultAction
{
    public readonly Record Record;

    public FetchRecordResultAction(Record record)
    {
        Record = record;
    }
}