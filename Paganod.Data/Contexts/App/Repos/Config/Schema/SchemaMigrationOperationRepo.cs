using Paganod.Data.Contexts.App;
using Paganod.Data.Schema.App.Schema;
using Paganod.Types.Domain;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Paganod.Data.Repos.Paganod.Config.Schema;

//internal class SchemaMigrationOperationRepo : RepoBase<MigrationOperation, SchemaMigrationOperationRecord>, ISchemaMigrationOperationRepo
//{
//    public SchemaMigrationOperationRepo(AppDbContext prmDbContext)
//        : base(prmDbContext)
//    {

//    }

//    public IEnumerable<MigrationOperation> GetAllForMigration(Guid prmMigrationNo)
//    {
//        foreach (var record in DbEf.SchemaMigrationOperationRecords.Where(x => x.SchemaMigrationId == prmMigrationNo))
//            yield return Mapper.Map<MigrationOperation>(record);

//        //return DbEf.SchemaMigrationOperationRecords.Where(x => x.SchemaMigrationId == prmMigrationNo).AsMany<MigrationOperation>();
//    }
//}
