using System.Collections.Generic;
using System.Threading.Tasks;
using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Paganod.Web.Client.Types;
using Paganod.Web.Client.Types.ViewModels;

namespace Paganod.Web.Client.Features.Config.Schema.Components;

public partial class SchemaEditor : ComponentBase
{
    [Parameter]
    public EditSchemaFormViewModel FormModel { get; set; }

    [Parameter]
    public EventCallback OnFormSubmit { get; set; }

    [CascadingParameter] 
    public IModalService Modal { get; set; }

    protected override Task OnParametersSetAsync()
    {
        return base.OnParametersSetAsync();
    }

    protected async Task ConfigureOptionsAsync(EditSchemaColumnViewModel col)
    {
        var modalParameters = new ModalParameters();
        var columnOptionsModal = Modal.Show<ColumnOptionsModal>($"Configure Column Options - {col.Name} - {col.Type}", modalParameters);
        var result = await columnOptionsModal.Result;

        if (!result.Cancelled && result?.Data is not null && result?.Data is IDictionary<string, string>)
            if ((result?.Data as IDictionary<string, string>)?.Count > 0)
                col.Options = result?.Data as IDictionary<string, string>; // result.Data will always be IDictionary<string, string> type
    }

    protected void FormSubmitted()
    {
        OnFormSubmit.InvokeAsync();
    }
}