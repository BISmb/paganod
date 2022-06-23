namespace Paganod.Web.Client.Types;

public record Attachment
{
    public string Name { get; set; }
    public Guid AttatchmentId { get; set; }
    public Guid FileId { get; set; }
    public string FileType { get; set; }

    public string RelatedRecordTableName { get; set; }
    public Guid RelatedRecordId { get; set; }

    public Attachment()
    {
        AttatchmentId = Guid.NewGuid();
        FileId = Guid.NewGuid();
    }
}

public record NewAttachment : Attachment
{
    public StreamContent FileContent { get; set; }

    public NewAttachment(StreamContent newFileContent)
        : base()
    {
        FileContent = newFileContent;
    }
}