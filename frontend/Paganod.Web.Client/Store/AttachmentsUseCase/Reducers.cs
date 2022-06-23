using System.Text.Json;
using Fluxor;

using Paganod.Web.Client.Types;
using Paganod.Web.Store.AttachmentsUseCase.Actions;
using Paganod.Web.Store.DataUseCase.Actions;

namespace Paganod.Web.Store.AttachmentsUseCase;

public static class Reducers
{
    [ReducerMethod]
    public static AttachmentsState ReduceFetchAttachmentsAction(AttachmentsState state, FetchAttachmentsAction action)
    {
        return state with { IsLoading = true, StatusMessage = "Loading related attachments..." };
    }

    [ReducerMethod]
    public static AttachmentsState ReduceFetchAttachmentsResultAction(AttachmentsState state, FetchAttachmentsResultAction action)
    {
        return state with { IsLoading = false, Attatchments = new List<Attachment>(action.Attachments) };
    }

    [ReducerMethod]
    public static AttachmentsState ReduceFetchRecordsResultAction(AttachmentsState state, FetchRecordsResultAction action)
    {
        return state with { IsLoading = false };
    }
}