using Paganod.Types.Base.Paganod;

using System;

namespace Paganod.Data.Schema.Paganod.Schema;

public sealed record SchemaColumnRecord() : DataRecord, ISchemaColumn
{
    public Guid SchemaModelId { get; set; }
    public string Name { get; set; }
    public FormFieldType Type { get; set; }
    public int? Version { get; set; }
    public string Alias { get; set; }
}