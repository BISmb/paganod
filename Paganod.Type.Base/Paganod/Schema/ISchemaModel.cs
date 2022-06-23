using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paganod.Types.Base.Paganod;

public interface ISchemaModel : IDataRecord
{
    string TableName { get; set; }
    //string TableDisplayName { get; set; }
    //string RecordName { get; set; }
    //string RecordDisplayName { get; set; }
    string PrimaryKeyName { get; set; }
    DbType PrimaryKeyType { get; set; }
    public bool Versioning { get; set; }
}
