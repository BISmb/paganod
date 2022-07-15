
using Paganod.Sql.Types;
using SqlKata;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Paganod.Sql.DML;

public interface IPaganodSqlKataGenerator
{
    PaganodSqlQuery Get(string tableName, Guid recordId);
    PaganodSqlQuery Get(string tableName, int page, int quantity);
    PaganodSqlQuery Get(string tableName, IReadOnlyDictionary<string, object> filters, int page, int quantity);
    //PaganodSqlQuery Get(ODataQuery oDataQuery);
    PaganodSqlQuery Delete(string tableName, Guid recordId);
    PaganodSqlQuery Update(string tableName, Guid recordId, IReadOnlyDictionary<string, object> data);
    PaganodSqlQuery Insert(string tableName, IReadOnlyDictionary<string, object> data);
    PaganodSqlQuery Count(string tableName, IDictionary<string, object> filters = null);


    PaganodSqlQuery UpdateColumn(string tableName, string columnSource, string columnDestination, string transformationExpression = "");
}

public static class SqlKataExtensions
{
    // todo: unit twst this method
    public static Query Select(this Query query, (string Name, string Alias)[] columnAndAliases)
    {
        query.Method = "select";

        var columns = columnAndAliases
            .Select(x => $"{x.Item1} as {x.Alias}")
            .ToArray();


        foreach (var column in columns)
        {
            query.AddComponent("select", new Column
            {
                Name = column
            });
        }

        return query;
    }

    public static Query AsUpdate(this Query query, string column, object value)
    {
        return query.AsUpdate(new string[] { column }, new object[] { value });
    }
}