using System.Text.Json;
using Fluxor;
using Paganod.Shared.Types;
using Paganod.Web.Client.Store.DataUseCase.Actions;
using Paganod.Web.Client.Types;
using Paganod.Web.Store.DataUseCase.Actions;

namespace Paganod.Web.Store.DataUseCase;

public static class Reducers
{
    [ReducerMethod]
    public static DataState ReduceFetchRecordsAction(DataState state, FetchRecordsAction action)
    {
        return state with { IsLoading = true, StatusMessage = "Loading Records..." };
    }

    [ReducerMethod]
    public static DataState ReduceFetchRecordsResultAction(DataState state, FetchRecordsResultAction action)
    {
        return state with { IsLoading = false, CurrentRecords = action.Records.ToList() };
    }

    [ReducerMethod]
    public static DataState ReduceFetchRecordAction(DataState state, FetchRecordAction action)
    {
        return state with { IsLoading = true, StatusMessage = "Loading Record..." };
    }

    [ReducerMethod]
    public static DataState ReduceFetchRecordResultAction(DataState state, FetchRecordResultAction action)
    {
        state.CurrentRecords.Add(action.Record);
        return state with { IsLoading = false, CurrentRecord = action.Record,  };
    }

    [ReducerMethod]
    public static DataState ReduceSetCurrentRecordAction(DataState state, SetCurrentRecordAction action)
    {
        Record currentRecord = null; //state.CurrentRecords.First(x => x.TableName == action.TableName && x.Id == (Guid)action.RecordId);
        return state with { CurrentRecord = currentRecord };
    }

    [ReducerMethod]
    public static DataState ReduceSaveRecordAction(DataState state, SaveRecordAction action)
    {
        return state with { IsLoading = true, StatusMessage = "Saving record..." };
    }

    [ReducerMethod]
    public static DataState ReduceSaveRecordAction(DataState state, SaveRecordResultAction action)
    {
        return state with { IsLoading = false, CurrentRecord = action.Record };
    }
}