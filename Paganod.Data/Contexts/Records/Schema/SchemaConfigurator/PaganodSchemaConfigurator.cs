using System;
using System.Threading.Tasks;
using Paganod.Data.Records.Schema;
using Paganod.Data.Shared.Interfaces;
using Paganod.Types.Base.Paganod.Schema;
using Paganod.Types.Domain;

namespace Paganod.Data.Contexts.Records.Schema;

internal class PaganodSchemaConfigurator : SchemaConfigurator, IPaganodSchemaConfigurator
{
    private readonly IAppDbContext Db;

    public PaganodSchemaConfigurator(IAppDbContext prmPaganodAppContext)
        : base(prmPaganodAppContext)
    {
        Db = prmPaganodAppContext;
    }

    public async Task<(SchemaMigration ForwardMigration, SchemaMigration SunsetMigration)> GeneratePaganodMigrationsAsync()
    {
        await Task.Delay(0);
        Guid targetSchemaId = Guid.Empty;

        if (_TargetTable is not null && Db.SchemaModels.Exists(_TargetTable))
            targetSchemaId = Db.SchemaModels.GetByTableName(_TargetTable).Id;

        var forwardMigration = new SchemaMigration(targetSchemaId, SchemaMigrationType.Forward);
        var sunsetMigration = new SchemaMigration(targetSchemaId, SchemaMigrationType.Sunset);

        /*
         * blue / green operations will already be generated at this point, so:
         *  
         *  - SchemaMigrationOperationType.RenameTable
         *  - SchemaMigrationOperationType.RenameColumn 
         *  - and SchemaMigrationOperationType.AlterColumn 
         *  
         *  should not have to be be handled
         *  Relationships will eventually invole Adding / Removing columns
         */

        foreach (var op in SqlOperations)
        {
            switch (op.OperationType)
            {
                case SchemaMigrationOperationType.CreateTable:
                case SchemaMigrationOperationType.AddColumn:
                    forwardMigration.Operations.Add(op);
                    break;

                case SchemaMigrationOperationType.DeleteTable:
                case SchemaMigrationOperationType.DeleteColumn:
                    sunsetMigration.Operations.Add(op);
                    break;

                default:
                    throw new Exception("Operation was not categorized when generating migrations");
            }
        }

        return sunsetMigration.Operations.Count > 0
            ? (forwardMigration, sunsetMigration)
            : (forwardMigration, null);
    }

    /// <summary>
    /// Runs validation on the operations in this migration to determine if they will be successful in the current database schema state
    /// </summary>
    /// <returns></returns>
    public async Task<bool> ValidatePreMigrationAsync(SchemaMigration migration)
    {
        // validate that migration will work as designed (not deleting a table that does not already exist, etc.)

        // ex. deleting table does not currently hold data without a user acceptance prompt
        // any column operations besides created a column (the column alread exists)
        await Task.Delay(1000);
        return true;
    }

}

