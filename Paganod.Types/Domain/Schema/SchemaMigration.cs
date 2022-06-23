using Paganod.Types.Base;
using Paganod.Types.Base.Paganod.Schema;

using System;
using System.Collections.Generic;

namespace Paganod.Types.Domain;

//public class Mapper_SchemaMigration : DualMapper<Migration, MigrationRecord>
//{
//    public override void MapT1ToT2(Migration srcObject, ref MigrationRecord dstObject)
//    {
//        DataRecord.MapCommonProperties(srcObject, ref dstObject);

//        dstObject.Type = srcObject.Type;
//        dstObject.ScheduledOn = srcObject.ScheduledOn;
//        dstObject.TargetSchemaModelId = srcObject.TargetSchemaModelId;
//        //dstObject.AppliedOn = srcObject.AppliedOn;
//    }

//    public override void MapT2ToT1(MigrationRecord srcObject, ref Migration dstObject)
//    {
//        DataRecord.MapCommonProperties(srcObject, ref dstObject);

//        dstObject.Type = srcObject.Type;
//        dstObject.ScheduledOn = srcObject.ScheduledOn;
//        dstObject.TargetSchemaModelId = srcObject.TargetSchemaModelId;
//        //dstObject.AppliedOn = srcObject.AppliedOn;
//    }

//    //public override void MapFrom(MigrationRecord srcObject, ref Migration dstObject)
//    //{
//    //    DataRecord.MapCommonProperties(srcObject, ref dstObject);

//    //    dstObject.Type = srcObject.Type;
//    //    dstObject.ScheduledOn = srcObject.ScheduledOn;
//    //    dstObject.TargetSchemaModelId = srcObject.TargetSchemaModelId;
//    //    //dstObject.AppliedOn = srcObject.AppliedOn;
//    //}

//    //public override void MapTo(Migration srcObject, ref MigrationRecord dstObject)
//    //{
        
//    //}
//}

public sealed record SchemaMigration : DomainType, ISchemaMigration
{
    public SchemaMigrationType Type { get; set; }
    public DateTime ScheduledOn { get; set; }
    public DateTime? AppliedOn { get; set; }
    public Guid TargetSchemaModelId { get; set; }

    public ICollection<SchemaMigrationOperation> Operations { get; set; }

    public SchemaMigration()
    {
        Operations = new List<SchemaMigrationOperation>();
    }

    public SchemaMigration(Guid targetSchemaId, SchemaMigrationType type)
        : this()
    {
        TargetSchemaModelId = targetSchemaId;
        Type = type;
    }
}

