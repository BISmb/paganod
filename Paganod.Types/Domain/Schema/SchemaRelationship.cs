using Paganod.Types.Base.Paganod;
using System;

namespace Paganod.Types.Domain;

public sealed record SchemaRelationship : DomainType, ISchemaRelationship
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
}
