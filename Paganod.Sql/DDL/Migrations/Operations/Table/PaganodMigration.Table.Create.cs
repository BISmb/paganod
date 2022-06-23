using Paganod.Data.Shared.Types;
using Paganod.Shared.Logic;
using Paganod.Sql.DDL.FluentMigrator;
using Paganod.Types.Base.Paganod;
using Paganod.Types.Domain;
using System.Threading.Tasks;

namespace Paganod.Sql.DDL.Migrations;

public partial class PaganodMigration
{
    private Task Handle_Table_Create(ICreateTableOperation operation)
    {
        string tableDbName = Common.FormatTableDatabaseName(operation.TableName);
        string tablePkName = Common.FormatPrimaryKeyDbName(operation.PrimaryKeyName);

        Create.Table(tableDbName)
              .WithColumn(tablePkName)
              .AsCustom(FluentMigratorMethods.GetDbType(operation.PrimaryKeyType, null, null))
              .PrimaryKey();

        var newSchemaModel = new SchemaModel
        {
            TableName = operation.TableName,
            Versioning = true,
        };

        var pkColumn = new SchemaColumn
        {
            Id = newSchemaModel.Id,
            Name = operation.PrimaryKeyName,
            Type = FormFieldType.Key,
        };
        newSchemaModel.Columns.Add(pkColumn);

        AppDbContext.SchemaModels.Add(newSchemaModel);
        //migration.AppDbContext.SchemaColumns.Add(pkColumn);

        SetTargetSchemaModel(newSchemaModel);

        return Task.CompletedTask;
    }
}
