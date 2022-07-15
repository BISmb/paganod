using Fluxor;

using Paganod.Web.Client.Store.SchemaUseCase;
using Paganod.Web.Store.SchemaUseCase.Actions;

using Paganod.Api.Shared.Feature.Config.Schema.Responses;
using Paganod.Api.Shared;
using Paganod.Api.Client;

namespace Paganod.Web.Store.SchemaUseCase;

public class Effects
{
    private readonly IPaganodApiClient PaganodApiClient;

    public Effects(IPaganodApiClient paganodApiClient)
    {
        PaganodApiClient = paganodApiClient;
    }

    [EffectMethod]
    public async Task HandleCreateSchemaAction(CreateSchemaAction action, IDispatcher dispatcher)
    {
        await Task.Delay(1000);

        // worth using polly try / catch here?
        var schemaId = Guid.Empty; // await PaganodApiClient.CreatSchemaAsync(action.Model);

        //if (schemaId is null)
        //    dispatcher.Dispatch(new CreateSchemaResultAction(null));
        //else
            dispatcher.Dispatch(new CreateSchemaResultAction(schemaId));
    }

    [EffectMethod]
    public async Task HandleFetchSchemasAction(FetchSchemasAction action, IDispatcher dispatcher)
    {
        var schemas = await PaganodApiClient.GetSchemaModels();

        dispatcher.Dispatch(new FetchSchemasResultAction(schemas));

    }

    [EffectMethod]
    public async Task HandleFetchConfigSchemaAction(FetchConfigSchemaAction action, IDispatcher dispatcher)
    {
        var schemaModel = action.SchemaId != Guid.Empty
            ? await PaganodApiClient.GetSchemaModelAsync(action.SchemaId)
            : await PaganodApiClient.GetSchemaModelAsync(action.SchemaTableName);

        dispatcher.Dispatch(new FetchConfigSchemaResultAction(schemaModel));
    }

    [EffectMethod]
    public async Task HandleSaveConfigSchemaAction(SaveConfigSchemaAction action, IDispatcher dispatcher)
    {
        // action.SchemaId, get full schema model for config
        //await Task.Delay(5000);

        // post to api endpoint

        // get migrationIds and provide

        dispatcher.Dispatch(new SaveConfigSchemaResultAction(Guid.Empty, Guid.Empty));
    }

    // [EffectMethod]
    // public async Task HandleSaveConfigSchemaAction(FetchSchemaListAction action, IDispatcher dispatcher)
    // {
    //     // action.SchemaId, get full schema model for config
    //     //await Task.Delay(1000);

    //     // post to api endpoint

    //     // get migrationIds and provide

    //     var tableNames = new string[] { "transactions" };
    //     dispatcher.Dispatch(new FetchSchemaListResultAction(tableNames));
    // }

    // [EffectMethod]
    // public async Task HandleFetchSchemaModelByTableNameAction(FetchSchemaModelByTableNameAction action, IDispatcher dispatcher)
    // {
    //     Console.WriteLine("Fetching schema model...");

    //     // action.SchemaId, get full schema model for config
    //     await Task.Delay(5000);

    //     var schemaModel = await PaganodApiClient.GetSchemaModelAsync(action.TableName);

    //     dispatcher.Dispatch(new FetchConfigSchemaResultAction(schemaModel));
    // }
}