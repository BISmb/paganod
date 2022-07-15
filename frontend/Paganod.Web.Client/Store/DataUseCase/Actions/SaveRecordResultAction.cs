using Paganod.Shared.Types;
using Paganod.Web.Client.Types;

namespace Paganod.Web.Client.Store.DataUseCase.Actions;

public class SaveRecordResultAction
{
    public readonly Record Record;

    public SaveRecordResultAction(Record newlySavedRecord)
    {
        Record = newlySavedRecord;
    }
}