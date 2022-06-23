using Paganod.Data.Shared.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paganod.Sql.DDL.Migrations;

public partial class PaganodMigration
{
    private Task Handle_Column_Rename(IRenameColumnOperation operation)
    {
        Rename.Column(operation.CurrentColumnName).OnTable(operation.TableName).To(operation.NewColumnName);

        var targetSchemaColumn = AppDbContext.SchemaColumns.GetByTableNameAndColumnName(operation.TableName, operation.NewColumnName);
        targetSchemaColumn.Name = operation.NewColumnName;

        return Task.CompletedTask;
    }
}
