using Paganod.Types.Base;
using Paganod.Types.Base.Paganod.Schema;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paganod.Data.Schema.App.Schema;

public sealed record MigrationRecord() : DataRecord<int>, ISchemaMigration
{
    public SchemaMigrationType Type { get; set; }
    public DateTime ScheduledOn { get; set; }
    public DateTime? AppliedOn { get; set; }
    public Guid TargetSchemaModelId { get; set; }
}
