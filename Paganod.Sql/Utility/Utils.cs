using Paganod.Types.Base.Paganod;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paganod.Sql.Utility;

internal static class Utils
{
    internal static DbType GetDbTypeFromFieldType(FormFieldType fieldType)
    {
        return fieldType switch
        {
            FormFieldType.CheckBox => DbType.Boolean,
            FormFieldType.Date => DbType.DateTime2,
            FormFieldType.DateTime => DbType.DateTime2,
            FormFieldType.Decimal => DbType.Decimal,
            FormFieldType.Dropdown => DbType.String,
            FormFieldType.Number => DbType.Int64,
            FormFieldType.Text => DbType.String,
            FormFieldType.Reference => DbType.Int64,

            _ => throw new NotImplementedException($"{fieldType}"),
        };
    }
}