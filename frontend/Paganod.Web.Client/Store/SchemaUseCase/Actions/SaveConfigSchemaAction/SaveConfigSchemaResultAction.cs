using Paganod.Web.Client.Types;
using Paganod.Api.Shared.Feature.Config.Schema.Responses;

namespace Paganod.Web.Store.SchemaUseCase.Actions;

public class SaveConfigSchemaResultAction
{
    public SchemaModelDto? SchemaModel { get; set; }
    public Guid ForwardMigrationId { get; set; }
    public Guid SunsetMigrationId { get; set; }

    public SaveConfigSchemaResultAction(SchemaModelDto updatedSchemaModelDto)
    {
        SchemaModel = updatedSchemaModelDto;
    }

    public SaveConfigSchemaResultAction(Guid forwardMigrationId, Guid sunsetMigrationId)
    {
        ForwardMigrationId = forwardMigrationId;
        SunsetMigrationId = sunsetMigrationId;
    }
}