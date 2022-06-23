using Paganod.Types.Base;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paganod.Data.Schema.Paganod.Files;

public sealed record ManagedFileRecord : DataRecord
{
    public Guid StorageConnectorId { get; set; }
    public string FileLocation { get; set; }
    public string FileName { get; set; }
    public string FileExtension { get; set; }
}