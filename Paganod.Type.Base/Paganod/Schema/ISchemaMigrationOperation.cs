using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paganod.Types.Base.Paganod.Schema;

public interface ISchemaMigrationOperation : IDataRecord
{
    Guid SchemaMigrationId { get; set; }
    SchemaMigrationOperationType OperationType { get; set; } // enum
    string Data { get; set; } // json data that can be used to  reconstruct the schema model
}

public enum SchemaMigrationOperationType
{
    CreateTable,
    RenameTable,
    DeleteTable,

    AddColumn,
    RenameColumn,
    AlterColumn,
    DeleteColumn,

    //AddRelationship,
    //AlterRelationship
    //DeleteRelationship
}
