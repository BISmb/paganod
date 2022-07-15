using System.Collections.ObjectModel;
using Fluxor;
using Paganod.Web.Client.Store.SchemaUseCase;
using Paganod.Web.Client.Types;
using Paganod.Web.Client.Types.ViewModels;
using Paganod.Web.Store.SchemaUseCase.Actions;

namespace Paganod.Web.Store.SchemaUseCase;

public static class Reducers
{
    [ReducerMethod]
    public static ConfigSchemaState ReduceCreateSchemaAction(ConfigSchemaState state, CreateSchemaAction action)
    {
        return state with { IsLoading = true, StatusMessage = "Creating Schema..." };
    }

    [ReducerMethod]
    public static ConfigSchemaState ReduceCreateSchemaResultAction(ConfigSchemaState state, CreateSchemaResultAction action)
    {
        return state with 
        {
            IsLoading = false, 
            StatusMessage = action.SchemaId == Guid.Empty 
                            ? "Schema Created Successfully" 
                            : "Creating Schema Failed" 
        };
    }

    [ReducerMethod]
    public static ConfigSchemaState ReduceFetchConfigSchemaAction(ConfigSchemaState state, FetchConfigSchemaAction action)
    {
        return state with { IsLoading = true, StatusMessage = "Loading schema..." };
    }

    [ReducerMethod]
    public static ConfigSchemaState ReduceFetchConfigSchemaResultAction(ConfigSchemaState state, FetchConfigSchemaResultAction action)
    {
        return state with { IsLoading = false, SchemaModel = action.SchemaModel };
    }

    [ReducerMethod]
    public static ConfigSchemaState ReduceFetchSchemas(ConfigSchemaState state, FetchConfigSchemaAction action)
    {
        return state with { IsLoading = true, StatusMessage = "Loading schema..." };
    }

    [ReducerMethod]
    public static ConfigSchemaState ReduceFetchSchemasResult(ConfigSchemaState state, FetchSchemasResultAction action)
    {
        return state with { SchemaModels = action.Schemas };
    }

    [ReducerMethod]
    public static ConfigSchemaState ReduceFetchConfigSchemaActionResult(ConfigSchemaState state, FetchConfigSchemaResultAction action)
    {
        var formModel = new EditSchemaFormViewModel
        {
            SolutionId = action.SchemaModel.SolutionId,
            TableName = action.SchemaModel.TableName,
            PrimaryKeyName = action.SchemaModel.PrimaryKeyName,
            PrimaryKeyType = action.SchemaModel.PrimaryKeyType,

            Columns = new ObservableCollection<EditSchemaColumnViewModel>(
                action.SchemaModel.Columns.Select(x => new EditSchemaColumnViewModel(x.Name, x.Type, x.ColumnId, x.Options))
            ),
        };

        return state with { IsLoading = false, SchemaModel = action.SchemaModel, Form = formModel };
    }

    [ReducerMethod]
    public static ConfigSchemaState ReduceSaveConfigSchemaAction(ConfigSchemaState state, SaveConfigSchemaAction action)
    {
        return state with { IsLoading = true };
    }

    [ReducerMethod]
    public static ConfigSchemaState ReduceSaveConfigSchemaResultAction(ConfigSchemaState state, SaveConfigSchemaResultAction action)
    {
        return state with { IsLoading = false, SchemaModel = action.SchemaModel, Form = null };
    }

    // [ReducerMethod]
    // public static ConfigSchemaState ReduceFetchSchemaListAction(ConfigSchemaState state, FetchSchemaListAction action)
    // {
    //     return state with { IsLoading = true, StatusMessage = "Getting list of table..." };
    // }

    // [ReducerMethod]
    // public static ConfigSchemaState ReduceFetchSchemaModelByTableNameAction(ConfigSchemaState state, FetchSchemaModelByTableNameAction action)
    // {
    //     Console.WriteLine("Reducing FetchSchemaModelByTableNameAction to Loading state...");
    //     return state with { IsLoading = true, StatusMessage = "Loading schema..." };
    // }

    // FetchSchemaModelByTableNameAction
}