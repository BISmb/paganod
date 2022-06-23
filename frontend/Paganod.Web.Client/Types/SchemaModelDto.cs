using Paganod.Types.Base.Paganod;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace Paganod.Web.Client.Types;

public record SchemaModelDto
{
    public Guid SolutionId { get; init; }
    public string TableName { get; init; }
    public string PrimaryKeyName { get; init; }
    public DbType PrimaryKeyType { get; init; }

    public ICollection<SchemaColumnDto> Columns { get; init; }

    public SchemaModelDto()
    {
        Columns = new List<SchemaColumnDto>();
        PrimaryKeyName = "id";
        PrimaryKeyType = DbType.Guid;
    }
}

public record SchemaColumnDto //: INotifyPropertyChanged
{
    public Guid ColumnId { get; set; }
    //public string DisplayName { get; set; }
    public string Name { get; set; }
    public FormFieldType Type { get; set; }
    public IDictionary<string, string> Options { get; set; } // TODO: change this to string and interpret it where needed

    public SchemaColumnDto()
    {
        ColumnId = Guid.Empty;
        Options = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    }

    public SchemaColumnDto(string colName, FormFieldType type) // add design type as a parameter for this constructor
        : this()
    {
        Name = colName;
        Type = type;
    }

    public SchemaColumnDto(string colName, FormFieldType type, Guid columnId = default, params (string, string)[] options) // add design type as a parameter for this constructor
        : this(colName, type)
    {
        ColumnId = columnId;

        foreach (var option in options)
            Options.Add(option.Item1, option.Item2);
    }

    public SchemaColumnDto(string colName, FormFieldType type, Guid columnId, IDictionary<string, string> options) // add design type as a parameter for this constructor
        : this(colName, type, columnId, options.Select(x => (x.Key, x.Value)).ToArray())
    {
    }
}

// public enum FormFieldType
// {
//     None,
//     Key,
//     CheckBox,
//     Number,
//     Decimal,
//     Date,
//     DateTime,
//     Dropdown, // Text but with Text Constraint
//     Function,
//     Reference, // Further Options: Relation Type: N:N, N:1, 1:N
//     Text, // Further Options: Masks
//     MaskedText,
//     MultiText
// }