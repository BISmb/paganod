using Paganod.Data.Shared.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paganod.Sql.DDL.Migrations;

public partial class PaganodMigration
{
    private Task Handle_Table_Rename(IRenameTableOperation operation)
    {
        return Task.CompletedTask;
    }
}