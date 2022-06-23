using Paganod.Data.Contexts.App;
using Paganod.Data.Schema.Paganod.Schema;
using Paganod.Data.Shared.Interfaces.Repos;
using Paganod.Types.Domain;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Paganod.Data.Repos.Paganod;

internal class SchemaColumnRepo : RepoBase<SchemaColumn, SchemaColumnRecord>, ISchemaColumnRepo
{
    public SchemaColumnRepo(AppDbContext prmDbContext)
        : base(prmDbContext)
    {

    }

    public IEnumerable<SchemaColumn> GetSchemaColumnsForSchemaModelId(Guid schemaId)
    {
        var schemaColumnRecords = DbEf.SchemaColumnRecords.Where(x => x.SchemaModelId == schemaId);
        //return schemaColumnRecords.AsMany<SchemaColumn>();

        foreach (var record in schemaColumnRecords)
            yield return Mapper.Map<SchemaColumn>(record);
    }

    public SchemaColumn GetByColumnName(string columnName)
    {
        var record = DbEf.SchemaColumnRecords.First(x => x.Name.Equals(columnName));
        //return record.As<SchemaColumn>();
        return Mapper.Map<SchemaColumn>(record);
    }

    public IEnumerable<SchemaColumn> GetSchemaColumnsForTableName(string tableName)
    {
        var schemaModelId = DbEf.SchemaModelRecords.First(x => x.TableName == tableName).Id;
        var schemaColumnRecords = DbEf.SchemaColumnRecords.Where(x => x.SchemaModelId == schemaModelId);
        //return schemaColumnRecords.AsMany<SchemaColumn>();

        foreach (var record in schemaColumnRecords)
            yield return Mapper.Map<SchemaColumn>(record);
        //return Mapper<SchemaColumn>(schemaColumnRecords);
    }

    public bool Exists(string tableName, string colName)
    {
        var schemaModelId = Db.SchemaModels.GetByTableName(tableName).Id;

        return DbEf.SchemaColumnRecords.Any(x => x.SchemaModelId == schemaModelId &&
                                        x.Name == colName);
    }

    public IEnumerable<SchemaColumn> GetByAliasName(string colAlias)
    {
        var schemaModelRecords = DbEf.SchemaColumnRecords.Where(x => x.Alias.Equals(colAlias));

        foreach (var record in schemaModelRecords)
            yield return Mapper.Map<SchemaColumn>(record);
    }

    public SchemaColumn GetByTableNameAndColumnName(string tableName, string columnName)
    {
        var schemaModelId = Db.SchemaModels.GetByTableName(tableName).Id;
        var schemaColumnRecord = DbEf.SchemaColumnRecords.First(x => x.SchemaModelId == schemaModelId & x.Name == columnName);
        return Mapper.Map<SchemaColumn>(schemaColumnRecord);
    }
}
