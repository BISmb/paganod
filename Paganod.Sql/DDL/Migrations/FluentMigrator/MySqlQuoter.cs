using FluentMigrator.Runner.Generators.MySql;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paganod.Config.Core.Sql._FluentMigrator
{
    internal class PaganodMySqlQuoter : MySqlQuoter
    {
        public override string QuoteTableName(string tableName, string schemaName = null)
        {
            //tableName = tableName.Clean();
            return base.QuoteTableName(tableName, schemaName);
        }

        public override string QuoteColumnName(string columnName)
        {
            //columnName = columnName.Clean();
            return base.QuoteColumnName(columnName);
        }
    }
}
