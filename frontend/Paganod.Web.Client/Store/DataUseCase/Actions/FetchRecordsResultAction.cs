using Paganod.Shared.Types;
using Paganod.Web.Client.Types;

namespace Paganod.Web.Store.DataUseCase.Actions;

public class FetchRecordsResultAction
{
    public IEnumerable<Record> Records { get; init; }

    public FetchRecordsResultAction(IEnumerable<Record> records)
    {
        Records = records;
    }
}