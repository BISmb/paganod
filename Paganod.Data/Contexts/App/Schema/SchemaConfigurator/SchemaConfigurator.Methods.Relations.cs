using Paganod.Data.Shared.Interfaces;
using Paganod.Types.Base.Paganod;
using System;

namespace Paganod.Data.App.Schema;

internal partial class SchemaConfigurator
{
    public ISchemaConfigurator AddRelationship(RelationshipType relationshipType, string referencedTable, ReferentialIntegrity referentialIntegrity = ReferentialIntegrity.None, string alternativePrincipalTable = null)
    {
        var principalSchema = Db.SchemaModels.GetByTableName(_TargetTable ?? alternativePrincipalTable);
        var referencedSchema = Db.SchemaModels.GetByTableName(referencedTable);

        // todo: add operation to spcefiy primary key / foreign key indexing (as a part of AddColumnOperation options)

        switch (relationshipType)
        {
            case RelationshipType.OneToOne:
                AddColumn(referencedSchema.PrimaryKeyName, FormFieldType.Reference);
                AddColumn(principalSchema.PrimaryKeyName, FormFieldType.Reference, alternativeTableName: referencedSchema.TableName);
                break;

            case RelationshipType.OneToMany:
                AddColumn(principalSchema.PrimaryKeyName, FormFieldType.Reference, alternativeTableName: referencedSchema.TableName);
                break;

            case RelationshipType.ManyToMany:
                // need to create a Intersection table with both primary key names,
                // then add a column to each table to match up the keys in the Intersection / Join table
                throw new NotImplementedException();

            case RelationshipType.ManyToOne:
                AddColumn(referencedSchema.PrimaryKeyName, FormFieldType.Reference);
                break;
        }

        return this;
    }

    public void RemoveRelationship(string principalTable, string referenceTable)
    {
        throw new NotImplementedException();
    }


}
