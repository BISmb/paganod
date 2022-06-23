using Paganod.Types.Base;
using Paganod.Types.Base.Paganod.Schema;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paganod.Data.Schema.App.Schema;

public sealed record SchemaMigrationOperationRecord : DataRecord, ISchemaMigrationOperation
{
    public Guid SchemaMigrationId { get; set; }
    public SchemaMigrationOperationType OperationType { get; set; }
    public string Data { get; set; }
}
