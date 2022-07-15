using System;
using System.Collections.Generic;
using System.Data;

namespace Paganod.Data.Records.Repo;

internal partial class DataRepository
{
    private object ParseType(object value, (string TableName, string ColName)? ColInfo = null)
    {
        if (Guid.TryParse(value?.ToString(), out Guid guidValue))
            return guidValue;

        return value;
    }
}
