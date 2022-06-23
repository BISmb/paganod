using Paganod.Data.Shared.Types;
using Paganod.Types.Base.Paganod.Schema;
using Paganod.Types.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Paganod.Sql.DDL.Migrations;

public partial class PaganodMigration
{
    internal void SetTargetSchemaModel(SchemaModel sm)
    {
        TargetSchemaModel = sm;
    }

    private IEnumerable<ISchemaOperation> GetFluentOperations(IEnumerable<SchemaMigrationOperation> prmOperations)
    {
        foreach (var operation in prmOperations)
            yield return ConvertToMigrationOperation(operation);
    }

    private ISchemaOperation ConvertToMigrationOperation(SchemaMigrationOperation operation)
    {
        return operation.OperationType switch
        {
            SchemaMigrationOperationType.CreateTable => JsonSerializer.Deserialize<CreateTableOperation>(operation.Data),
            SchemaMigrationOperationType.RenameTable => JsonSerializer.Deserialize<RenameTableOperation>(operation.Data),
            SchemaMigrationOperationType.DeleteTable => JsonSerializer.Deserialize<DeleteTableOperation>(operation.Data),

            SchemaMigrationOperationType.AddColumn => JsonSerializer.Deserialize<AddColumnOperation>(operation.Data),
            SchemaMigrationOperationType.RenameColumn => JsonSerializer.Deserialize<RenameColumnOperation>(operation.Data),
            SchemaMigrationOperationType.AlterColumn => JsonSerializer.Deserialize<AlterColumnOperation>(operation.Data),
            SchemaMigrationOperationType.DeleteColumn => JsonSerializer.Deserialize<RemoveColumnOperation>(operation.Data),

            _ => throw new Exception($"Unknown Operation Type: {operation.OperationType}"),
        };
    }

    private void Handle(ISchemaOperation schemaOperation)
    {
        Task operationTask = schemaOperation switch
        {
            ICreateTableOperation => Handle_Table_Create(schemaOperation as ICreateTableOperation),
            IDeleteTableOperation => Handle_Table_Delete(schemaOperation as IDeleteTableOperation),
            IRenameTableOperation => Handle_Table_Rename(schemaOperation as IRenameTableOperation),

            IAddColumnOperation => Handle_Column_Add(schemaOperation as IAddColumnOperation),
            IRemoveColumnOperation => Handle_Column_Remove(schemaOperation as IRemoveColumnOperation),
            IRenameColumnOperation => Handle_Column_Rename(schemaOperation as IRenameColumnOperation),
            IAlterColumnOperation => Handle_Column_Alter(schemaOperation as IAlterColumnOperation),

            _ => throw new NotImplementedException($"{schemaOperation.GetType().Name} is not implemented"),
        };

        operationTask.Wait();
    }
}
