using Paganod.Data.Contexts.App;
using Paganod.Data.Schema.Paganod.Schema;
using Paganod.Data.Shared.Interfaces.Repos;
using Paganod.Types.Base.Paganod;
using Paganod.Types.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Paganod.Data.Repos.Paganod;

internal class SchemaModelRepo : RepoBase<SchemaModel, SchemaModelRecord>, ISchemaModelRepo
{
    public SchemaModelRepo(AppDbContext prmDbContext)
        : base(prmDbContext)
    {

    }

    public override void Add(SchemaModel domainObject)
    {
        base.Add(domainObject);

        foreach (var c in domainObject.Columns)
        {
            if (c.SchemaModelId != domainObject.Id)
                c.SchemaModelId = domainObject.Id;

            Db.SchemaColumns.Add(c);
        }
    }

    public bool Any(Expression<Func<SchemaModelRecord, bool>> wherePredicate)
    {
        return DbEf.SchemaModelRecords.Any(wherePredicate);
    }

    public IEnumerable<SchemaModel> Where(Expression<Func<SchemaModelRecord, bool>> wherePredicate)
    {
        var schemaModelRecords = DbEf.SchemaModelRecords.Where(wherePredicate);

        foreach (var record in schemaModelRecords)
            yield return Mapper.Map<SchemaModel>(record);
    }

    public override void AddRange(IEnumerable<SchemaModel> domainObjects)
    {
        base.AddRange(domainObjects);

        foreach (var domainObject in domainObjects)
            foreach (var c in domainObject.Columns)
                Db.SchemaColumns.Add(c);
    }

    public SchemaModel GetByTableName(string tableName)
    {
        var record = DbEf.SchemaModelRecords.FirstOrDefault(x => x.TableName == tableName);
        //return record.As<SchemaModel>();
        return Mapper.Map<SchemaModel>(record);
    }

    public string GetTableNameFromSchemaId(Guid schemaId)
    {
        var dObject = Get(schemaId);
        return dObject.TableName;
    }

    public override SchemaModel GetFull(Guid id)
    {
        var dObject = Get(id);

        dObject.Columns = Db.SchemaColumns.GetSchemaColumnsForSchemaModelId(dObject.Id).ToList();
        dObject.Relations = Db.SchemaRelationships.GetSchemaRelationshipsForSchemaModelId(dObject.Id).ToList();

        return dObject;

    }

    public override IEnumerable<SchemaModel> GetAllFull()
    {
        IList<SchemaModel> schemaModels = new List<SchemaModel>();

        var smallSchemaModels = GetAll();

        foreach (var sm in smallSchemaModels)
            schemaModels.Add(GetFull(sm.Id));

        // for each schema model, build up the domain model

        return schemaModels;
    }

    public bool Any(Expression<Func<ISchemaModel, bool>> predicate)
    {
        return DbEf.SchemaModelRecords.Cast<ISchemaModel>().Any(predicate);
    }

    public SchemaModel GetByRecordType(string recordType)
    {
        throw new NotImplementedException();
    }

    public bool Exists(string sqlTableName)
    {
        return DbEf.SchemaModelRecords.Any(x => x.TableName == sqlTableName);
    }

    public IEnumerable<string> GetAllTableNames()
    {
        return DbEf.SchemaModelRecords.Select(x => x.TableName);
    }

    public string GetPrimaryKeyForTable(string tableName)
    {
        return DbEf.SchemaModelRecords.First(x => x.TableName.Equals(StringComparer.OrdinalIgnoreCase)).PrimaryKeyName;
    }
}
