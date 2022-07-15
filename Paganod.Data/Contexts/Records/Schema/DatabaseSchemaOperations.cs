using Microsoft.Data.Sqlite;
using Paganod.Data.Contexts.Records.Schema;
using Paganod.Data.Shared.Interfaces;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paganod.Data.Records.Schema;

internal class DatabaseSchemaOperations : IDatabaseSchemaOperations
{
    public ISchemaReader Read => new SchemaReader(_GetDbConnection());
    public IPaganodSchemaConfigurator Configure => new PaganodSchemaConfigurator(_DbContext);


    private readonly IAppDbContext _DbContext;
    private readonly Func<DbConnection> _GetDbConnection;

    internal DatabaseSchemaOperations(IAppDbContext dbContext, Func<DbConnection> prmGetDbConnection)
    {
        _DbContext = dbContext;
        _GetDbConnection = prmGetDbConnection;
    }
}
