//using Paganod.Data.App;
//using Paganod.Data.Contexts.App;
//using Paganod.Data.Schema.App.Schema;
//using Paganod.Data.Shared.Interfaces.Repos;
//using Paganod.Types.Base.Paganod.Schema;
//using Paganod.Types.Domain;

//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace Paganod.Data.Repos.Paganod.Config.Schema;

//internal class SchemaMigrationRepo : RepoBase<Migration, MigrationRecord>, ISchemaMigrationRepo
//{
//    public SchemaMigrationRepo(AppDbContext prmDbContext)
//        : base(prmDbContext)
//    {
//        Db = prmDbContext;
//    }

//    public override void Add(Migration domainObject)
//    {
//        base.Add(domainObject);

//        foreach (var operation in domainObject.Operations)
//        {
//            operation.SchemaMigrationId = domainObject.Id;
//            Db.SchemaMigrationOperations.Add(operation);
//        }
//    }

//    public Migration GetMigration(Guid id)
//    {
//        var record = DbEf.SchemaMigrationRecords.First(r => r.Id == id);
//        var domainObj = Mapper.Map<Migration>(record);
//        domainObj.PropertyChanged += DomainObject_PropertyChanged;
//        return domainObj;
//    }

//    /// <summary>
//    /// Will get any migrations that have not been applied and are scheduled to execute
//    /// </summary>
//    /// <param name="migrationType"></param>
//    /// <returns></returns>
//    public IEnumerable<Migration> GetUnappliedMigrations(SchemaMigrationType migrationType)
//    {
//        var migrations = new List<Migration>();
//        var migrationRecords = Db.DataAccessLayer.SchemaMigrationRecords.Where(x => x.Type == migrationType &&
//                                                                    (x.ScheduledOn <= DateTime.Now || x.ScheduledOn == DateTime.MinValue) &&
//                                                                    !(x.AppliedOn.HasValue)).ToArray();

//        foreach (var migrationRecord in migrationRecords)
//        {
//            //var migration = migrationRecord.As<Migration>();
//            var migration = Mapper.Map<Migration>(migrationRecord);
//            migration.Operations = Db.SchemaMigrationOperations.GetAllForMigration(migration.Id).ToArray();
//            migration.PropertyChanged += DomainObject_PropertyChanged;
//            migrations.Add(migration);
//        }

//        return migrations;
//    }
//}
