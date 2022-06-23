using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paganod.Types.Base;

public interface IDataRecord : IDataRecord<Guid> 
{ 

}

public interface IDataRecord<T>
{
    T Id { get; set; }
    DateTime CreatedOn { get; set; }
    DateTime ModifiedOn { get; set; }
}