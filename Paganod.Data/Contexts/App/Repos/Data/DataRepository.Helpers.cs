using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paganod.Data.App.Repos.Data;

internal partial class DataRepository
{
    /// <summary>
    /// Get TableName if RecordType was provided
    /// </summary>
    /// <param name="recordType"></param>
    /// <returns></returns>
    private string GetTableName(string recordType)
    {
        if (_SchemaReader.ContainsTable(recordType))
            return recordType; // this is a tableName

        return _SchemaReader.GetTableNameFromRecordType(recordType);
    }

    /// <summary>
    /// Get Primary Key for a TableName
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    private string GetPrimaryKeyName(string tableName)
    {
        return _SchemaReader.GetPrimaryKeyNameForTable(tableName);
    }
}
