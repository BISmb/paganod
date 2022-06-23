using Paganod.Data.Shared.Types;
using Paganod.Sql.DDL.FluentMigrator;
using Paganod.Sql.Utility;
using Paganod.Types.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paganod.Sql.DDL.Migrations;

public partial class PaganodMigration
{
    private Task Handle_Column_Alter(IAlterColumnOperation operation)
    {
        var dbType = Utils.GetDbTypeFromFieldType(operation.PaganodType);
        string strDbType = FluentMigratorMethods.GetDbType(dbType, null, null);

        Alter.Column(operation.Name).OnTable(operation.TableName).AsCustom(strDbType);

        return Task.CompletedTask;
    }
}