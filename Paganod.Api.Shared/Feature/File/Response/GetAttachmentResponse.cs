using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paganod.Api.Shared.Feature.File.Response;

public class GetAttachmentResponse
{
    public string Name { get; set; }
    public string FileType { get; set; }
    public Guid ManagedFileId { get; set; }

    public string AssociatedRecordType { get; set; }
    public Guid AssociatedRecordId { get; set; }
}