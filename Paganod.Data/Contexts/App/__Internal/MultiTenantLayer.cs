using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Paganod.Data.App.Internal;

using System;
using System.Linq.Expressions;

namespace Paganod.Data.Contexts.App.Internal;

internal sealed class MultiTenantLayer : EfDataAccess
{
    public MultiTenantLayer(DbContextOptions<EfDataAccess> options)
        : base(options)
    {

    }

    internal readonly Guid TenantId;

    protected override void ApplyBaseConfigurationPerEntity(ref EntityTypeBuilder oEntityBuilder)
    {
        oEntityBuilder.Property(typeof(Guid), nameof(TenantId));
        var matchTenantExpression = Expression.Lambda(Expression.Property(Expression.Constant(TenantId), nameof(TenantId)));

        oEntityBuilder.HasQueryFilter(matchTenantExpression);
        //oEntityBuilder.HasQueryFilter(x => EF.Property<Guid>(x, nameof(TenantId)) == TenantId);

        base.ApplyBaseConfigurationPerEntity(ref oEntityBuilder);
    }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    // add tenant id
    //    modelBuilder.Entity<SchemaModelRecord>().Property(typeof(Guid), nameof(TenantId));
    //    modelBuilder.Entity<SchemaModelRecord>().HasQueryFilter(x => EF.Property<Guid>(x, nameof(TenantId)) == TenantId);

    //    base.OnModelCreating(modelBuilder);
    //}
}
