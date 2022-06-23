
using System;

namespace Paganod.Data.Schema.Files;

public sealed record AttachmentRecord : DataRecord
{
    public string RecordType { get; set; }
    public Guid RecordId { get; set; }
    public Guid ManagedFileId { get; set; }
}
