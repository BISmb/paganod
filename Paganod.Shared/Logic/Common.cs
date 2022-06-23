using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Paganod.Shared.Logic;

public static class Common
{
    public static string FormatTableDatabaseName(string tableName)
    {
        return FormatDatabaseObjectName(tableName);
    }

    public static string FormatPrimaryKeyDbName(string primaryKeyName)
    {
        return FormatDatabaseObjectName(primaryKeyName);
    }

    private static string FormatDatabaseObjectName(string name)
    {
        name = name.Replace("-", "_").ToLower();

        var match = Regex.Match(name, "[a-z_0-9]*");

        if (!match.Success)
            throw new Exception("Regex did not have any matches. TableDbName");

        return match.Value;
    }

    public static string SerializeToJson(object o)
    {
        var options = Json.Json.GetSharedJsonOptions(true);
        string json = JsonSerializer.Serialize(o, options);
        return json;
    }
}