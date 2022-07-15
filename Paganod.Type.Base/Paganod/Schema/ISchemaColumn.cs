using System;
using System.Data;

namespace Paganod.Types.Base.Paganod;

public interface ISchemaColumn : IDataRecord
{
    public Guid SchemaModelId { get; set; }
    public string Name { get; set; }
    public FormFieldType Type { get; set; }
    public int? Version { get; set; }
    public string Alias { get; set; }
}

public enum FormFieldType
{
    None,
    Key,
    CheckBox,
    Number,
    Decimal,
    Date,
    DateTime,
    Dropdown, // Text but with Text Constraint
    //Function,
    Reference, // Further Options: Relation Type: N:N, N:1, 1:N
    Text, // Further Options: Masks
    MaskedText,
    MultiText
}

public static class PaganodTypeMap
{
    public static (FormFieldType PaganodType, DbType DatabaseType, Type ClrType)[] TypeMap = new (FormFieldType PaganodType, DbType DatabaseType, Type ClrType)[] 
    {
        (FormFieldType.Text, DbType.String, typeof(string)),
    };
}