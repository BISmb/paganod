using Paganod.Data.Shared.Types;
using System.Linq;
using System.Threading.Tasks;

namespace Paganod.Sql.DDL.Migrations;

// destructivve change, should only happen during sunset migrations
public partial class PaganodMigration
{
    private Task Handle_Column_Remove(IRemoveColumnOperation operation)
    {
        string tableName = operation.TableName.ToLower();
        string colName = operation.Name.ToLower();

        Delete.Column(colName).FromTable(tableName);

        var columnToDelete = AppDbContext.SchemaColumns.GetByColumnName(colName);

        // if this is a rename scenario or change tpy scenario, there will be two columns with the same Alias
        var similarAliasedColumns = AppDbContext.SchemaColumns.GetByAliasName(colName);
        if (similarAliasedColumns.Count() > 1)
        {
            var targetReplacementColumn = similarAliasedColumns.First(x => x.Name != x.Alias);
            targetReplacementColumn.Version = null;
            Rename.Column(targetReplacementColumn.Alias).OnTable(tableName);

            columnToDelete = similarAliasedColumns.First(x => x.Name == x.Alias);
        }

        AppDbContext.SchemaColumns.Remove(columnToDelete);

        return Task.CompletedTask;
    }
}