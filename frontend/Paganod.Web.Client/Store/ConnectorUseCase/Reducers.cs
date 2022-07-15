using System;
using System.Linq;
using System.Linq.Expressions;
using Fluxor;
using Paganod.Web.Client.Store.ConnectorUseCase.Actions;

namespace Paganod.Web.Client.Store.ConnectorUseCase;

public static class Reducers
{
    [ReducerMethod]
    public static ConnectorState ReduceFetchConnectorsAction(ConnectorState state, FetchConnectorsAction action)
    {
        return state with { IsLoading = true, StatusMessage = "Loading Connectors..." };
    }

    [ReducerMethod]
    public static ConnectorState ReduceFetchConnectorsResultsAction(ConnectorState state, FetchConnectorsActionResults action)
    {
        var mergedConnectors = state.Connectors.Merge(action.Connectors, nameof(ConnectorDto.ConnectorId));

        return state with { Connectors = mergedConnectors };
    }
}

public static class LinqExtensions
{
    public static ICollection<T> Merge<T>(this ICollection<T> list1, IEnumerable<T> list2, string propertyName)
    {
        var matchFunc = new Func<T, T, bool>((item1, item2) =>
        {
            var val1 = typeof(T).GetProperty(propertyName)?.GetValue(item1);
            var val2 = typeof(T).GetProperty(propertyName)?.GetValue(item2);

            if (val1 is null || val2 is null)
                throw new ArgumentNullException("Cannot compare with a null value");

            return val1 == val2;
        });

        return list1.Merge(list2, matchFunc);
    }

    public static ICollection<T> Merge<T>(this ICollection<T> list1, IEnumerable<T> list2, Func<T, T, bool> matchFunc)
    {
        Console.WriteLine(list1.Count);

        var mergedList = new List<T>(list1);
        
        foreach (var newConnector in list2)
        {
            var matchedElement = mergedList.First(x => matchFunc(x, newConnector));

            if (matchedElement is not null)
                matchedElement = newConnector;
            else
                mergedList.Add(newConnector);
        }

        return mergedList;
    }
}

