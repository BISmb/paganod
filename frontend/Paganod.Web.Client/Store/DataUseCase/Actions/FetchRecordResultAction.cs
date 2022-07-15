using Paganod.Shared.Types;

namespace Paganod.Web.Store.DataUseCase.Actions;

public class FetchRecordResultAction
{
    public Record Record { get; init; }

    public FetchRecordResultAction(Record record)
    {
        Record = record;
    }
}