using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paganod.Data.Shared.Interfaces;

public interface IDataContext
{
    int SaveChanges(CancellationToken cancellationToken = default(CancellationToken));
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
}
