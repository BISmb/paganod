
using Paganod.Data.Shared.Interfaces;
using Paganod.Sql.DDL.FluentMigrator;
using Paganod.Sql.Utility;
using Paganod.Types.Domain;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Paganod.Sql.DDL.Migrations;

public class SchemaConfigRunner
{
    private readonly IAppDbContext PaganodDb;
    private readonly IDbConnection TargetDb;

    public SchemaConfigRunner(IAppDbContext appDbContext, IDbConnection targetDb)
    {
        PaganodDb = appDbContext;
        TargetDb = targetDb;
    }

    public async Task RunAsync(SchemaMigration schemaMigration)
    {
        var taskCompletionSource = new TaskCompletionSource();
        Run(schemaMigration, taskCompletionSource);
        await taskCompletionSource.Task;
        await PaganodDb.SaveChangesAsync();
    }

    private void Run(SchemaMigration migration, TaskCompletionSource taskCompletionSource)
    {
        try
        {
            var paganodMigration = new PaganodMigration(PaganodDb, migration);
            var runner = FluentMigratorMethods.GetMigrationRunner(TargetDb);
            runner.Up(paganodMigration);
            taskCompletionSource.SetResult();
        }
        catch (Exception ex)
        {
            taskCompletionSource.SetException(ex);
        }
    }
}
