using System.Text.Json;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Paganod.Web.Features.Config.Schema.Pages;
using Paganod.Web.Store.DataUseCase;
using Paganod.Web.Store.SchemaUseCase;
using Paganod.Web.Features.Data.Components;
using Blazored.Modal;
using Blazored.Modal.Services;
using Paganod.Web.Client.Types;
using System.Threading.Tasks;
using System.Linq;

namespace Paganod.Web.Features.Data.Components;

public partial class PaganodInput : FluxorComponent
{
    // Inject Data State
    [Inject]
    private IState<ConfigSchemaState> SchemaState { get; set; }

    [Inject]
    private IState<DataState> DataState { get; set; }

    [CascadingParameter] 
    public IModalService Modal { get; set; }

    [Parameter]
    public string Label { get; set; }

    [Parameter]
    public string FieldName { get; set; }

    // [Parameter]
    // public Paganod.Shared.Enums.Design.FieldType Type { get; set; }

    [Parameter]
    public object Value { get; set; }

    [Parameter]
    public EventCallback<object> ValueChanged { get; set; }

    protected SchemaColumnDto TargetColumnSchema { get; set; }

    protected override Task OnParametersSetAsync()
    {
        SetPaganodType();

        return base.OnParametersSetAsync();
    }

    private void SetPaganodType()
    {
        if (SchemaState.Value.SchemaModel.Columns.Any(x => x.Name == FieldName)) // should always return true because it is fetched previously? otherwise just need to wait untill it is returned?
            TargetColumnSchema = SchemaState.Value.SchemaModel.Columns.First(x => x.Name == FieldName);

        StateHasChanged();
    }

    protected void HandleValueChange(ChangeEventArgs args)
    {
        SetValue(args.Value);
    }

    private void SetValue(object val)
    {
        Value = val;
        ValueChanged.InvokeAsync(Value);
        StateHasChanged();
    }

    protected async Task OpenLookupModal()
    {
        string referencedTableName = TargetColumnSchema.Options["ReferencedTable"];

        var parameters = new ModalParameters();
        parameters.Add(nameof(LookupModal.TableName), referencedTableName);

        var recordSelectionModal = Modal.Show<LookupModal>($"Select an {referencedTableName}", parameters);
        var result = await recordSelectionModal.Result;
        
        SetValue(result.Data);
    }
}