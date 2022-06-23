using Paganod.Types.Base.Paganod.Schema;

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Paganod.Data.Shared.Interfaces;

namespace Paganod.Data.Contexts.App;

internal partial class AppDbContext : IAppDbContext
{
    public DbCommand GetDbCommand()
    {
        throw new NotImplementedException();
    }

    public ISchemaConfigurator GetSchemaConfigue()
    {
        throw new NotImplementedException();
    }

    public IDbConnection NewConnection()
    {
        throw new NotImplementedException();
    }

    public IDbConnection GetUserDatabase()
    {
        throw new NotImplementedException();
    }

    public Task RunUnappliedMirationsAsync(SchemaMigrationType migrationsType)
    {
        throw new NotImplementedException();
    }

    public virtual int SaveChanges(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return DataAccessLayer.SaveChangesAsync();
    }
}