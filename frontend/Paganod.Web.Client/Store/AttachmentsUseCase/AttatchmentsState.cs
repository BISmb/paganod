using Fluxor;

using Paganod.Web.Client.Types;

namespace Paganod.Web.Store.AttachmentsUseCase;

/*
    Attatchments will be for a current record only. The current record is whatever is displayed in the RecordFormPage.razor
*/

[FeatureState]
public record AttachmentsState : StateWithBackgroundProcessing
{
    public ICollection<Attachment> Attatchments { get; init; }

    private AttachmentsState() 
    {
        Attatchments = new List<Attachment>();
    }
}