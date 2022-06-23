using Paganod.Web.Client.Types;

namespace Paganod.Web.Store.MetadataUseCase.Actions;

public class RefreshMetadataResultAction
{
    public readonly SchemaMetadataNavigationObject[] SchemaModelsMetadata;

    public RefreshMetadataResultAction(SchemaMetadataNavigationObject[] schemaModelMetadataObject)
    {
        SchemaModelsMetadata = schemaModelMetadataObject;
    }
}