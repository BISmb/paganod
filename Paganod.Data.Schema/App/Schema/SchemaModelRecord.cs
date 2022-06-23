using Paganod.Types.Base;
using Paganod.Types.Base.Paganod;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paganod.Data.Schema.Paganod.Schema;

public sealed record SchemaModelRecord : DataRecord, ISchemaModel
{
    public string TableName { get; set; }
    //public string TableDisplayName { get; set; }
    //public string RecordName { get; set; }
    //public string RecordDisplayName { get; set; }
    public string PrimaryKeyName { get; set; }
    public System.Data.DbType PrimaryKeyType { get; set; }
    public bool Versioning { get; set; }

}