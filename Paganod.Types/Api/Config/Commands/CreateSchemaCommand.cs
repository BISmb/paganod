using System.Data;

namespace Paganod.Types.Api.Config.Commands;

public sealed record CreateSchemaCommand
{
    public string? TableName { get; set; }
    public string? PrimaryKeyName { get; set; }
    public DbType? PrimaryKeyType { get; set; }
    public AddColumnCommand[]? NewColumns { get; set; }
}