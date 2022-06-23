using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paganod.Types.Base.Paganod.Schema;

public interface ISchemaMigration
{
    Guid TargetSchemaModelId { get; set; }
    SchemaMigrationType Type { get; set; }
    DateTime ScheduledOn { get; set; }
    DateTime? AppliedOn { get; set; }
}

public enum SchemaMigrationType
{
    Forward, // ForwardChanges
    Sunset // SunsetChanges
}
