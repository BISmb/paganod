using System.Data;

namespace Paganod.Api.Shared.Feature.Config.Schema.Commands;

public record CreateSchemaCommand
{
    public string? TableName { get; set; }
    public string? PrimaryKeyName { get; set; }
    public DbType? PrimaryKeyType { get; set; }
    public AddColumnCommand[]? NewColumns { get; set; }
}