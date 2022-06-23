using Paganod.Shared.Types;
using Paganod.Types;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paganod.Data.Shared.Interfaces.Repos;

public interface IRecordRepository
{
    Task<Record> GetAsync(string tableName, Guid id);
    Task<Record[]> GetAsync(string tableName, int pageNumber, int resultsPerPage);
    Task<bool> TryGetAsync(string tableName, Guid id, out Record result);
    Task<int> SaveAsync(string tableName, IDictionary<string, object> data);
    Task<int> DeleteAsync(string tableName, Guid id);
    Task<bool> ExistsAsync(string tableName, Guid id);



    //Task<T> RunQuery();
}

/// <summary>
/// Interface for operations specifi to one table
/// </summary>
//public interface ITargetTableDataOperations
//{
//    string Table { get; }

//    Task<Record> GetAsync(TableName, Guid id);
//    Task<Record[]> GetAsync(string tableName, int pageNumber, int resultsPerPage);
//    Task<bool> TryGetAsync(string tableName, Guid id, out Record result);
//    Task<int> SaveAsync(string tableName, IDictionary<string, object> data);
//    Task<int> DeleteAsync(string tableName, Guid id);
//    Task<bool> ExistsAsync(string tableName, Guid id);

//    Task<IEnumerable<IDictionary<string, object>>> AllRecordsAsync();
//    Task<int> SaveAsync(IDictionary<string, object> newData);
//}
