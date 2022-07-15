using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paganod.Types.Base.Paganod;

namespace Paganod.Api.Shared.Feature.Config.Schema.Responses;

#nullable enable

public sealed record SchemaModelDto
{
    public Guid SolutionId { get; init; }
    public Guid SchemaModelId { get; init; }
    public string TableName { get; init; }
    public string PrimaryKeyName { get; init; }
    public DbType PrimaryKeyType { get; init; }

    public ICollection<SchemaColumnDto>? Columns { get; init; }

    public SchemaModelDto()
    {
        Columns = new List<SchemaColumnDto>();
    }
}

public sealed record SchemaColumnDto //: INotifyPropertyChanged
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