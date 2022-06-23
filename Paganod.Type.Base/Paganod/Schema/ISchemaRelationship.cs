using System;

namespace Paganod.Types.Base.Paganod;

public interface ISchemaRelationship : IDataRecord
{
    Guid PrincipalSchemaId { get; set; }
    string PrincipalSchemaName { get; set; }
    string PrincipalSchemaPrimaryKeyColumnName { get; set; }
    Guid RelatedSchemaId { get; set; }
    string RelatedSchemaName { get; set; }
    string RelatedSchemaPrimaryKeyColumnName { get; set; }
    RelationshipType RelationshipType { get; set; }
    bool EnforceReferentialIntegrity { get; set; }
    bool OnDeleteCascade { get; set; }
}

public enum RelationshipType
{
    OneToOne,
    OneToMany,
    ManyToMany,
    ManyToOne
}

public enum ReferentialIntegrity
{
    None
}