using Paganod.Web.Client.Types;
using Paganod.Web.Client.Types.ViewModels;
using System.Data;

namespace Paganod.Web.Store.SchemaUseCase.Actions;

public class SaveConfigSchemaAction
{
    public readonly EditSchemaFormViewModel CompletedFormModel; 

    public SaveConfigSchemaAction(EditSchemaFormViewModel completedFormModel)
    {
        CompletedFormModel = completedFormModel;
    }
}