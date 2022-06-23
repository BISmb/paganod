using Paganod.Data.Shared.Interfaces;
using Paganod.Data.Shared.Types;
using Paganod.Types.Base.Paganod.Schema;
using Paganod.Types.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

using Migration = FluentMigrator.Migration;

namespace Paganod.Sql.DDL.Migrations;

public partial class PaganodMigration : Migration, IDisposable
{
    private readonly SchemaMigration SelectedMigration;

    private SchemaModel TargetSchemaModel;
    private readonly IAppDbContext AppDbContext;

    private readonly IEnumerable<ISchemaOperation> Operations;

    public PaganodMigration(IAppDbContext dbContext, SchemaMigration migration)
    {
        AppDbContext = dbContext;
        SelectedMigration = migration;

        if (migration.TargetSchemaModelId != Guid.Empty)
            if (AppDbContext.SchemaModels.Exists(migration.TargetSchemaModelId))
                TargetSchemaModel = AppDbContext.SchemaModels.GetFull(migration.TargetSchemaModelId);

        Operations = GetFluentOperations(migration.Operations).ToList();
        Operations = Operations.OrderBy(x => x.GetType() != typeof(CreateTableOperation));
    }

    public override void Down()
    {
        // rolling back migrations are not supported. Instead a complete database roll back must be done. Paganod Schema can be refreshed to match the state of the rolled back database
        throw new NotImplementedException();
    }

    public override void Up()
    {
        foreach (var operation in Operations)
            Handle(operation);
    }

    public void Dispose()
    {
        //throw new NotImplementedException();
    }
}