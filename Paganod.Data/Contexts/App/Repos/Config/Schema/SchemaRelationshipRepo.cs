using Paganod.Data.Contexts.App;
using Paganod.Data.Schema.Paganod.Schema;
using Paganod.Data.Shared.Interfaces.Repos;
using Paganod.Types.Domain;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Paganod.Data.Repos.Paganod;

internal class SchemaRelationshipRepo : RepoBase<SchemaRelationship, SchemaRelationshipRecord>, ISchemaRelationshipRepo
{
    public SchemaRelationshipRepo(AppDbContext prmDbContext) : base(prmDbContext)
    {

    }

    public IEnumerable<SchemaRelationship> GetSchemaRelationshipsForSchemaModelId(Guid id)
    {
        var relationShipRecords = DbEf.SchemaRelationshipRecords.Where(r => r.PrincipalSchemaId == id);

        foreach (var relationshipRecord in relationShipRecords)
            yield return Mapper.Map<SchemaRelationship>(relationshipRecord);
    }
}
