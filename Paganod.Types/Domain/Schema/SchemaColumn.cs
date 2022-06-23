using Paganod.Types.Base;
using Paganod.Types.Base.Paganod;

using System;

namespace Paganod.Types.Domain;

//public class SchemaColumn_Mapper : DualMapper<SchemaColumn, SchemaColumnRecord>
//{
//    public override void MapT2ToT1(SchemaColumnRecord srcObject, ref SchemaColumn dstObject)
//    {
//        DataRecord.MapCommonProperties(srcObject, ref dstObject);

//        dstObject.SchemaModelId = srcObject.SchemaModelId;
//        dstObject.Name = srcObject.Name;
//        dstObject.Type = srcObject.Type;

//        //dstObject.DisplayName = srcObject.DisplayName;
//        //dstObject.IsRequired = srcObject.IsRequired;
//        //dstObject.OptionsJson = srcObject.OptionsJson;
//        //dstObject.Version
//    }

//    public override void MapT1ToT2(SchemaColumn srcObject, ref SchemaColumnRecord dstObject)
//    {
//        DataRecord.MapCommonProperties(srcObject, ref dstObject);

//        dstObject.SchemaModelId = srcObject.SchemaModelId;
//        dstObject.Name = srcObject.Name;
//        dstObject.Type = srcObject.Type;
//    }
//}

public sealed record SchemaColumn() : DomainType, ISchemaColumn
{
    public SchemaColumn(Guid targetSchemaId, string colName, string colAlias, FormFieldType colType)
        : this()
    {
        SchemaModelId = targetSchemaId;
        Name = colName;
        Alias = colAlias;
        Type = colType;
    }

    public Guid SchemaModelId { get; set; }
    public string Name { get; set; }
    public FormFieldType Type { get; set; }
    public int? Version { get; set; }
    public string Alias { get; set; }
}
