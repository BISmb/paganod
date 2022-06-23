using Microsoft.Extensions.Configuration;
using Paganod.Data.App.Repos.Data;
using Paganod.Data.App.Schema;
using Paganod.Data.Contexts.App;
using Paganod.Data.Shared.Interfaces;
using Paganod.Data.Shared.Interfaces.Repos;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paganod.Data.Contexts.AppB;

public class AppBDbContext : IAppDbConnection
{
    public IRecordRepository Data { get; init; }
    public IDatabaseSchemaOperations Schema { get; init; }

    private readonly IAppDbContext _PaganodDbContext;
    private readonly IDbConnectionFactory _DbFactory;

    public AppBDbContext(IAppDbContext prmPaganodDbContext, (Type Type, string String) prmConnection)
    {
        _PaganodDbContext = prmPaganodDbContext;
        _DbFactory = new PaganodDbConnectionFactory(prmConnection.Type, prmConnection.String);
        
        Schema = new DatabaseSchemaOperations(_PaganodDbContext, _DbFactory);
        Data = new DataRepository(Schema.Read, _DbFactory);
    }

    public DbConnection NewConnection()
    {
        return _DbFactory.NewConnection();
    }
}