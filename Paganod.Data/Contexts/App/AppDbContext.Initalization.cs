using Paganod.Data.Shared.Interfaces;
using Paganod.Sql.DDL.Migrations;
using Paganod.Types.Base.Paganod.Schema;
using Paganod.Types.Domain;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace Paganod.Data.Contexts.App;

internal partial class AppDbContext //: IAppDbContext
{
    public async Task InitalizeAsync()
    {
        await DataAccessLayer.Database.EnsureDeletedAsync();
        await DataAccessLayer.Database.EnsureCreatedAsync();

        //// example: Create "transactions" table
        //var schemaConfigurator = new SchemaConfigurator(this);
        //schemaConfigurator.CreateTable("transactions")
        //                  .AddColumn("columnA", Paganod.Shared.Enums.Design.FieldType.Text);


        //return Task.CompletedTask;
    }

    public async Task RunUnAppliedMirationsAsync(SchemaMigrationType migrationsType)
    {
        var migrations = SchemaMigrations.GetUnappliedMigrations(migrationsType).ToArray();
        
        foreach (var migration in migrations)
        {
            await ExecuteMigrationOnTargetAsync(migration);
            await SaveChangesAsync();
        }
    }

    public Task ExecuteMigrationAsync(Guid migrationId)
    {
        var targetMigration = SchemaMigrations.GetFull(migrationId);
        return ExecuteMigrationOnTargetAsync(targetMigration);
    }

    public async Task ExecuteMigrationOnTargetAsync(SchemaMigration migration)
    {
        if (migration.AppliedOn != DateTime.MinValue)
            await Task.CompletedTask;

        //using (var targetDatabaseConnection = targetDatabase.NewConnection())
        //{
        //    SchemaConfigRunner configRunner = new SchemaConfigRunner(this, targetDatabaseConnection);
        //    await configRunner.RunAsync(migration);
        //}

        migration.AppliedOn = DateTime.Now;
        await SaveChangesAsync();
        //Schema.Read.Refresh(); // todo: ?
    }

    public IDbConnectionFactory AppDbFactory((Type DbType, string ConnectionString) Connection)
    {
        return new PaganodDbConnectionFactory(Connection.DbType, Connection.ConnectionString);
    }
}
