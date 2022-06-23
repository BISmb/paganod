using Paganod.Types.Base;
using Paganod.Types.Base.Paganod.Schema;

using System;

namespace Paganod.Types.Domain;

public sealed record SchemaMigrationOperation() : DomainType, ISchemaMigrationOperation
{
    public Guid SchemaMigrationId { get; set; }
    public SchemaMigrationOperationType OperationType { get; set; }
    public string Data { get; set; }

    public SchemaMigrationOperation(SchemaMigrationOperationType newOperationType, string jsonSerializedData)
        : this()
    {
        OperationType = newOperationType;
        Data = jsonSerializedData;
    }
}

//public class Mapper_SchemaMigrationOperation : DualMapper<MigrationOperation, SchemaMigrationOperationRecord>
//{
//    public override void MapT2ToT1(SchemaMigrationOperationRecord srcObject, ref MigrationOperation dstObject)
//    {
//        DataRecord.MapCommonProperties(srcObject, ref dstObject);

//        dstObject.SchemaMigrationId = srcObject.SchemaMigrationId;
//        dstObject.OperationType = srcObject.OperationType;
//        dstObject.Data = srcObject.Data;
//    }

//    public override void MapT1ToT2(MigrationOperation srcObject, ref SchemaMigrationOperationRecord dstObject)
//    {
//        DataRecord.MapCommonProperties(srcObject, ref dstObject);

//        dstObject.SchemaMigrationId = srcObject.SchemaMigrationId;
//        dstObject.OperationType = srcObject.OperationType;
//        dstObject.Data = srcObject.Data;
//    }
//}
