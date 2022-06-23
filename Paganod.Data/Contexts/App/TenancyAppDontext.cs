using Microsoft.EntityFrameworkCore;

using Paganod.Data.App;
using Paganod.Data.App.Internal;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paganod.Data.Contexts.App;

internal class TenancyAppDontext : AppDbContext
{
    public TenancyAppDontext(DbContextOptions<EfDataAccess> dbOptions)
        : base(dbOptions)
    {

    }
}
