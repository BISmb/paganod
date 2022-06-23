using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Paganod.Types.Base;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Paganod.Data.Schema;

namespace Paganod.Data.App.Internal;

public partial class EfDataAccess : DbContext
{
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        GenerateOnUpdate();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        GenerateOnUpdate();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void GenerateOnUpdate()
    {
        foreach (EntityEntry entityEntry in ChangeTracker.Entries())
        {
            if (entityEntry.GetType().GetProperty(nameof(DataRecord.CreatedOn)) is not null &&
                entityEntry.State == EntityState.Added)
                ((DataRecord)entityEntry.Entity).CreatedOn = DateTime.Now;

            if (entityEntry.GetType().GetProperty(nameof(DataRecord.ModifiedOn)) is not null &&
                entityEntry.State == EntityState.Modified)
                ((DataRecord)entityEntry.Entity).ModifiedOn = DateTime.Now;




            //if (!entityEntry.Entity.GetType().IsSubclassOf(typeof(DataRecord)))
            //    continue;

            //var record = entityEntry.Entity as DataRecord;

            //_ = entityEntry.State switch
            //{
            //    EntityState.Added => record = RecordCreated(record),
            //    EntityState.Modified => record = RecordModified(record),

            //    _ => null,
            //};
        }
    }

    private DataRecord RecordCreated(DataRecord record)
    {
        record.CreatedOn = DateTime.UtcNow;
        record.ModifiedOn = DateTime.UtcNow;

        return record;
    }

    private DataRecord RecordModified(DataRecord record)
    {
        record.ModifiedOn = DateTime.UtcNow;

        return record;
    }
}