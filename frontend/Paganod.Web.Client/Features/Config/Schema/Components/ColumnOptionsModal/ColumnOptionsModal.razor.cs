using System;
using System.Collections.Generic;
using System.Linq;
using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Paganod.Web.Client.Types.ViewModels;

namespace Paganod.Web.Client.Features.Config.Schema.Components;

public partial class ColumnOptionsModal : ComponentBase
{
    // [Parameter]
    // public EditSchemaColumnViewModel Column { get; set; }

    public IDictionary<string, string> Options { get; set; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    [CascadingParameter] 
    BlazoredModalInstance ModalInstance { get; set; }

    private void RenameOptionKey(KeyValuePair<string, string> CurrentOption, ChangeEventArgs args)
    {
        string optionName = args.Value.ToString();

        Options.Add(optionName, CurrentOption.Value);
        Options.Remove(CurrentOption.Key);
    }

    private void AddNewOption()
    {
        var newOption = ($"Option{Options.Count}", "");
        Options.Add("", "");
    }

    public void SaveOptions()
    {
        Options = Options.Where(x => !String.IsNullOrWhiteSpace(x.Key) && !String.IsNullOrWhiteSpace(x.Value))
                                       .ToDictionary(x => x.Key, x => x.Value);

        ModalInstance.CloseAsync(ModalResult.Ok(Options));
    }
}