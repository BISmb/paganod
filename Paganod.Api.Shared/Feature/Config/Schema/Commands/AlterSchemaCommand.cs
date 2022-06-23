using Paganod.Types.Base.Paganod;
using System;
using System.Collections.Generic;

namespace Paganod.Api.Shared.Feature.Config.Schema.Commands;

public record AlterSchemaCommand
{
    public Guid SchemaId { get; init; }
    public string TableName { get; set; }
    public AddColumnCommand[] AddedColumns { get; set; }
    public AlteredColumnCommand[] AlteredColumns { get; set; } // 
    public RenamedColumnCommand[] RenamedColumns { get; set; } // Rename is kind of an oxy-moran for versioning because it will consist of Adding New Column, Copying over Data, then Deleting Old Column
    public Guid[] DeletedColumns { get; set; }
}

public record AddColumnCommand
{
    public string Name { get; set; }
    public FormFieldType Type { get; set; }
    public Dictionary<string, string>? Options { get; set; } = new Dictionary<string, string>();
}

public record RenamedColumnCommand
{
    public Guid ColumnId { get; set; }
    public string NewName { get; set; }
}

public record AlteredColumnCommand
{
    public Guid ColumnId { get; set; }
    public FormFieldType Type { get; set; }
    public IDictionary<string, object> Options { get; set; } = new Dictionary<string, object>();
}
