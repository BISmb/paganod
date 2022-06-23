using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paganod.Sql.Types;

public struct PaganodSqlQuery
{
    public string Sql;
    public IDictionary<string, object> Parameters;

    public PaganodSqlQuery(string newSql,
                           IDictionary<string, object> newSqlParams)
    {
        Sql = newSql;
        Parameters = newSqlParams;
    }


    public static implicit operator PaganodSqlQuery(string sql) => new PaganodSqlQuery(sql, new Dictionary<string, object>());
}
