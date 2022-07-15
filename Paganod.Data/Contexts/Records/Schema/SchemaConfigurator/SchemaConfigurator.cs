using Paganod.Data.Shared.Interfaces;
using Paganod.Types.Base.Paganod;
using Paganod.Types.Base.Paganod.Schema;
using Paganod.Types.Domain;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Paganod.Data.Records.Schema;

/// <summary>
/// A class that has methods to configure the underlying database schema. Migrations can be generated and applied through this class
/// </summary>
internal partial class SchemaConfigurator : ISchemaConfigurator
{
    private readonly IAppDbContext Db;
    protected string _TargetTable { get; init; }
    protected IList<SchemaMigrationOperation> SqlOperations { get; init; }

    public SchemaConfigurator(IAppDbContext prmDbContext)
    {
        Db = prmDbContext;
        SqlOperations = new List<SchemaMigrationOperation>();
    }

    public ISchemaConfigurator ForTable(string tableName)
    {
        
        return new SchemaConfigurator(Db) { _TargetTable = tableName, SqlOperations = SqlOperations };
    }

    public void Dispose()
    {
        SqlOperations.Clear();
    }
}