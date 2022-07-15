using System;
using System.Data.Common;
using Paganod.Data.Records.Repo;
using Paganod.Data.Records.Schema;
using Paganod.Data.Shared.Interfaces;
using Paganod.Data.Shared.Interfaces.Repos;

namespace Paganod.Data.Contexts.Records;

public class RecordDbContext : IRecordDbContext
{
    private readonly IAppDbContext _appDbContext;
    public RecordDbContext(IAppDbContext prmAppDbContext)
    {
        _appDbContext = prmAppDbContext;
    }

    public IDatabaseSchemaOperations Schema
    {
        get => new DatabaseSchemaOperations(_appDbContext, NewConnection);
    }
    
    public IRecordRepository Data
    {
        get => new DataRepository(Schema.Read, NewConnection);
    }

    public void Dispose()
    {
        _appDbContext.Dispose();
    }

    public DbConnection NewConnection()
    {
        // get connection from AppDbContext info
        return null;
    }
}

