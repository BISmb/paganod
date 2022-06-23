
using Paganod.Sql.Types;
using System;
using System.Collections.Generic;

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