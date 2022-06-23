using Paganod.Types.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paganod.Data.Schema;

public abstract record DataRecord : DataRecord<Guid>, IDataRecord
{
    public DataRecord()
        : base()
    {
        Id = Guid.NewGuid();
    }
}

public abstract record DataRecord<IdType> : IDataRecord<IdType>
    where IdType : struct
{
    public IdType Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
}