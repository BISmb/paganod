using Microsoft.EntityFrameworkCore;

using Paganod.Data.App.Internal;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paganod.Data.Contexts.App.__Internal;

internal class SingleDatabaseLayer : EfDataAccess
{
    public SingleDatabaseLayer(DbContextOptions<EfDataAccess> options)
    : base(options)
    {

    }
}
