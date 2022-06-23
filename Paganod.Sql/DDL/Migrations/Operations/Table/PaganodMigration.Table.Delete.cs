using Paganod.Data.Shared.Types;
using System.Threading.Tasks;

namespace Paganod.Sql.DDL.Migrations;

public partial class PaganodMigration
{
    private Task Handle_Table_Delete(IDeleteTableOperation operation)
    {
        return Task.CompletedTask;
    }
}