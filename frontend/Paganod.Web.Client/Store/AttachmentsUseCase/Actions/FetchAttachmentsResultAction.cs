using Paganod.Web.Client.Types;

namespace Paganod.Web.Store.AttachmentsUseCase.Actions;

public class FetchAttachmentsResultAction
{
    public readonly IEnumerable<Attachment> Attachments;

    public FetchAttachmentsResultAction(IEnumerable<Attachment> attachments)
    {
        Attachments = attachments;
    }
}