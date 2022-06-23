namespace Paganod.Web.Client.Types;

public record SchemaMetadataNavigationObject
{
    public System.Guid SchemaId { get; set; }
    public string TableName { get; set; }
    public string DisplayTableName { get; set; }

    public string TableDataNavigationLink => $"data/{TableName}";
    public string TableConfigNavigationLink => $"config/schema/{SchemaId}";
}