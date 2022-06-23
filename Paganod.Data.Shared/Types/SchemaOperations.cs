using Paganod.Types.Base.Paganod;
using Paganod.Types.Domain;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Paganod.Data.Shared.Types;

public interface ISchemaOperation
{

}

public interface ICreateTableOperation : ISchemaOperation
{
    string TableName { get; init;}
    string PrimaryKeyName { get; init; }
    DbType PrimaryKeyType { get; init; }
}

public interface IRenameTableOperation : ISchemaOperation
{
    string CurrentTableName { get; init; }
    string NewTableName { get; init; }
}

public struct RenameTableOperation : IRenameTableOperation
{
    public string CurrentTableName { get; init; }
    public string NewTableName { get; init; }
}


public interface IDeleteTableOperation : ISchemaOperation
{
    string TableName { get; init; }
}

public struct DeleteTableOperation : IDeleteTableOperation
{
    public string TableName { get; init; }
}

public interface IAlterColumnOperation : ISchemaOperation
{
    string TableName { get; init; }
    string Name { get; init; }
    FormFieldType PaganodType { get; init; }
    Dictionary<string, string> Options { get; init; }
    string TransformationExpression { get; init; }
}

public struct AlterColumnOperation : IAlterColumnOperation
{
    public string TableName { get; init; }
    public string Name { get; init; }
    public FormFieldType PaganodType { get; init; }
    public Dictionary<string, string> Options { get; init; }
    public string TransformationExpression { get; init; }
}


public interface IAddColumnOperation : ISchemaOperation
{
    string TableName { get; init; }
    string Name { get; init; }
    FormFieldType PaganodType { get; init; }
    Dictionary<string, string> Options { get; init; }
    string Alias { get; init; }
}

public interface IRenameColumnOperation : ISchemaOperation
{
    string TableName { get; init; }
    string CurrentColumnName { get; init; }
    string NewColumnName { get; init; }
}

public struct RenameColumnOperation : IRenameColumnOperation
{
    public string TableName { get; init; }
    public string CurrentColumnName { get; init; }
    public string NewColumnName { get; init; }
}

public interface IRemoveColumnOperation : ISchemaOperation
{
    string TableName { get; init; }
    string Name { get; init; }
}

public struct CreateTableOperation : ICreateTableOperation
{
    public string TableName { get; init; }
    public string PrimaryKeyName { get; init; }
    public DbType PrimaryKeyType { get; init; }

    [JsonConstructor]
    public CreateTableOperation(string TableName,
                                string PrimaryKeyName,
                                DbType PrimaryKeyType)
    {
        this.TableName = TableName;
        this.PrimaryKeyName = PrimaryKeyName;
        this.PrimaryKeyType = PrimaryKeyType;
    }
}

public struct AddColumnOperation : IAddColumnOperation
{
    public string TableName { get; init; }
    public string Name { get; init; }
    public FormFieldType PaganodType { get; init; }
    public Dictionary<string, string> Options { get; init; }
    public string Alias { get; init; }

    [JsonConstructor]
    public AddColumnOperation(string TableName,
                              string Name,
                              FormFieldType PaganodType,
                              Dictionary<string, string> Options = null,
                              string Alias = null)
    {
        this.TableName = TableName;
        this.Name = Name;
        this.PaganodType = PaganodType;
        this.Options = Options ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        this.Alias = Alias ?? Name;
    }
}

public struct RemoveColumnOperation : IRemoveColumnOperation
{
    public string TableName { get; init; }
    public string Name { get; init; }

    [JsonConstructor]
    public RemoveColumnOperation(string TableName,
                                 string Name)
    {
        this.TableName = TableName;
        this.Name = Name;
    }
}