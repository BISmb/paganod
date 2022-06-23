using Paganod.Types.Base.Paganod;

using System;

namespace Paganod.Data.Schema.Paganod.Schema;

public sealed record SchemaRelationshipRecord() : DataRecord
{
    public Guid PrincipalSchemaId { get; set; }
    public string PrincipalSchemaName { get; set; }
    public string PrincipalSchemaPrimaryKeyColumnName { get; set; }
    public Guid RelatedSchemaId { get; set; }
    public string RelatedSchemaName { get; set; }
    public string RelatedSchemaPrimaryKeyColumnName { get; set; }
    public RelationshipType RelationshipType { get; set; }
    public bool EnforceReferentialIntegrity { get; set; }
    public bool OnDeleteCascade { get; set; }

    public SchemaRelationshipRecord(ISchemaRelationship srcObject)
        : this()
    {
        PrincipalSchemaId = srcObject.PrincipalSchemaId;
        PrincipalSchemaName = srcObject.PrincipalSchemaName;
        PrincipalSchemaPrimaryKeyColumnName = srcObject.PrincipalSchemaPrimaryKeyColumnName;
        RelatedSchemaId = srcObject.RelatedSchemaId;
        RelatedSchemaName = srcObject.RelatedSchemaName;
        RelatedSchemaPrimaryKeyColumnName = srcObject.RelatedSchemaPrimaryKeyColumnName;
        RelationshipType = srcObject.RelationshipType;
        EnforceReferentialIntegrity = srcObject.EnforceReferentialIntegrity;
        OnDeleteCascade = srcObject.OnDeleteCascade;
    }
}