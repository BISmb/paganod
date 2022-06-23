using Paganod.Web.Client.Types;

namespace Paganod.Web.Store.DataUseCase.Actions;

public class SaveRecordAction
{
    public readonly Record Record;

    public SaveRecordAction(Record recordToSave)
    {
        Record = recordToSave;
    }
}