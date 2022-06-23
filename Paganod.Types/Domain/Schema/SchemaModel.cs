using Paganod.Types.Base;
using Paganod.Types.Base.Paganod;

using System;
using System.Collections.Generic;
using System.Data;

namespace Paganod.Types.Domain;

//public class SchemaModel_Mapper : DualMapper<SchemaModel, SchemaModelRecord>
//{
//    public override void MapT2ToT1(SchemaModelRecord srcObject, ref SchemaModel dstObject)
//    {
//        DataRecord.MapCommonProperties(srcObject, ref dstObject);

//        dstObject.TableName = srcObject.TableName;
//        //dstObject.TableDisplayName = srcObject.TableDisplayName;
//        //dstObject.RecordName = srcObject.RecordName;
//        //dstObject.RecordDisplayName = srcObject.RecordDisplayName;
//        //dstObject.PrimaryKeyColumnName = srcObject.PrimaryKeyColumnName;
//        //dstObject.PrimaryKeyColumnType = srcObject.PrimaryKeyColumnType;
//        dstObject.Versioning = srcObject.Versioning;
//    }

//    public override void MapT1ToT2(SchemaModel srcObject, ref SchemaModelRecord dstObject)
//    {
//        DataRecord.MapCommonProperties(srcObject, ref dstObject);

//        dstObject.TableName = srcObject.TableName;
//        //dstObject.TableDisplayName = srcObject.TableDisplayName;
//        //dstObject.RecordName = srcObject.RecordName;
//        //dstObject.RecordDisplayName = srcObject.RecordDisplayName;
//        //dstObject.PrimaryKeyColumnName = srcObject.PrimaryKeyColumnName;
//        //dstObject.PrimaryKeyColumnType = srcObject.PrimaryKeyColumnType;
//        dstObject.Versioning = srcObject.Versioning;
//    }
//}

public sealed record SchemaModel : DomainType, ISchemaModel
{
    public string TableName { get; set; }
    //public string TableDisplayName { get; set; }
    //public string RecordName { get; set; }
    //public string RecordDisplayName { get; set; }
    //public string PrimaryKeyColumnName { get; set; }
    //public DbType PrimaryKeyColumnType { get; set; }
    public bool Versioning { get; set; }

    public ICollection<SchemaColumn> Columns { get; set; } // should be ICollection
    public ICollection<SchemaRelationship> Relations { get; set; } // should be ICollection
    public string PrimaryKeyName { get; set; }
    public DbType PrimaryKeyType { get; set; }

    public SchemaModel()
        : base()
    {
        Columns = new List<SchemaColumn>();
        Relations = new List<SchemaRelationship>();
    }

    public SchemaModel(Guid? schemaId = null)
        : this()
    {
        if (schemaId is not null)
            Id = schemaId.Value;
    }
}