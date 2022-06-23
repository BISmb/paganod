using Microsoft.Data.Sqlite;
using Paganod.Data.Shared.Interfaces;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paganod.Data.App.Schema;

internal class DatabaseSchemaOperations : IDatabaseSchemaOperations
{
    public ISchemaReader Read => new SchemaReader(_AppDbFactory.NewConnection());
    public ISchemaConfigurator Configure => new SchemaConfigurator(_DbContext);


    private readonly IAppDbContext _DbContext;
    private readonly IDbConnectionFactory _AppDbFactory;

    internal DatabaseSchemaOperations(IAppDbContext dbContext, IDbConnectionFactory prmAppDbConectionFactory)
    {
        _DbContext = dbContext;
        _AppDbFactory = prmAppDbConectionFactory;
    }
}
