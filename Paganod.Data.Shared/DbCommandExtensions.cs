using System.Data;

namespace Paganod.Data.Shared;

public static class DbCommandExtensions
{
    public static void AddParameters(this IDbCommand dbCommand, IDictionary<string, object> newSqlParams)
    {
        foreach (var param in newSqlParams)
        {
            var commandParameter = dbCommand.CreateParameter();
            commandParameter.ParameterName = param.Key;
            commandParameter.Value = param.Value;
            commandParameter.DbType = GetDbTypeFromObject(param.Value);
            dbCommand.Parameters.Add(commandParameter);
        }
    }

    private static DbType GetDbTypeFromObject(object value)
    {
        // todo: may be able to replace this with something native from Dapper?
        return DbTypeResolver.GetDbTypeFromClrType(value.GetType());
    }
}
