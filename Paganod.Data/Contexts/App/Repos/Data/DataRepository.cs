using Paganod.Data.Shared.Interfaces;
using Paganod.Data.Shared.Interfaces.Repos;
using Paganod.Shared.Types;
using Paganod.Sql.DML;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Paganod.Data.App.Repos.Data;

internal partial class DataRepository : IRecordRepository
{
    private readonly ISchemaReader _SchemaReader;
    private readonly IDbConnectionFactory _DbConnectionFactory;
    private readonly IPaganodSqlKataGenerator _SqlGenerator;

    /// <summary>
    /// Provides methods to retrieve data from the target database connection. Requires a schema reader to provide schema for queries
    /// </summary>
    /// <param name="targetDbConnection"></param>
    /// <param name="schemaReader"></param>
    public DataRepository(ISchemaReader schemaReader, IDbConnectionFactory prmDbConnectionFactory)
    {
        _SchemaReader = schemaReader;
        _DbConnectionFactory = prmDbConnectionFactory;
        _SqlGenerator = new SqlKataGenerator(schemaReader);
    }

    /// <summary>
    /// Get Records by recordType and Id
    /// </summary>
    /// <param name="recordType"></param>
    /// <param name="id"></param>
    /// <param name="schemaOwner"></param>
    /// <returns></returns>
    public async Task<Record> GetAsync(string recordType, Guid id)
    {
        var record = new Record(recordType);

        var tableName = GetTableName(recordType);
        var query = _SqlGenerator.Get(tableName, id);
        var rowData = await QueryFirstAsync(query);

        if (rowData is null)
            return null;

        record.AddMany(rowData);
        return record;
    }

    /// <summary>
    /// Get Records by recordType (by pages)
    /// </summary>
    /// <param name="recordType"></param>
    /// <param name="Id"></param>
    /// <param name="schemaOwner"></param>
    /// <returns></returns>
    public async Task<Record[]> GetAsync(string recordType, int pageNumber, int resultCount)
    {
        var records = new List<Record>();

        var tableName = GetTableName(recordType);
        var query = _SqlGenerator.Get(tableName, pageNumber, resultCount);
        var rowsData = await QueryAsync(query);

        foreach (var row in rowsData)
            records.Add(new Record(recordType, row));

        var result = await Task.FromResult(records.ToArray());
        return result;
    }

    /// <summary>
    /// Delete a record by id
    /// </summary>
    /// <param name="recordType"></param>
    /// <param name="recordId"></param>
    /// <returns></returns>
    public Task<int> DeleteAsync(string recordType, Guid recordId)
    {
        var tableName = GetTableName(recordType);
        var query = _SqlGenerator.Delete(tableName, recordId);
        
        var result = ExecuteNonQuery(query);
        return Task.FromResult(result);
    }

    /// <summary>
    /// Insert a record
    /// </summary>
    /// <param name="recordType"></param>
    /// <param name="recordId"></param>
    /// <returns></returns>
    private Task<int> InsertAsync(string recordType, IDictionary<string, object> data)
    {
        var tableName = GetTableName(recordType);

        var query = _SqlGenerator.Insert(tableName, new ReadOnlyDictionary<string, object>(data));
        var result = ExecuteNonQuery(query);
        return Task.FromResult(result);
    }

    /// <summary>
    /// Update a record
    /// </summary>
    /// <param name="recordType"></param>
    /// <param name="recordId"></param>
    /// <returns></returns>
    private Task<int> UpdateAsync(string recordType, Guid recordId, IDictionary<string, object> data)
    {
        var tableName = GetTableName(recordType);

        var query = _SqlGenerator.Update(tableName, recordId, new ReadOnlyDictionary<string, object>(data));
        var result = ExecuteNonQuery(query);
        return Task.FromResult(result);
    }

    /// <summary>
    /// Get a count of records returned from a query
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="filters"></param>
    /// <returns></returns>
    public async Task<int> CountAsync(string recordTypeOrTableName, IDictionary<string, object> filters = null)
    {
        recordTypeOrTableName = GetTableName(recordTypeOrTableName);
        var query = _SqlGenerator.Count(recordTypeOrTableName, filters);

        var result = await ExecuteScalarAsync<int>(query);
        return result;
    }

    /// <summary>
    /// Save a record. (Performs an INSERT or UPDATE operation if the record exists)
    /// </summary>
    /// <param name="recordType"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public async Task<int> SaveAsync(string recordType, IDictionary<string, object> data)
    {
        var tableName = GetTableName(recordType);
        var primaryKeyName = GetPrimaryKeyName(tableName);
        var recordId = Guid.Parse($"{data[primaryKeyName]}");

        return await ExistsAsync(tableName, recordId)
            ? await UpdateAsync(tableName, recordId, data)
            : await InsertAsync(tableName, data);
    }

    public async Task<bool> ExistsAsync(string recordType, Guid id)
    {
        var tableName = GetTableName(recordType);
        var primaryKeyName = GetPrimaryKeyName(tableName);

        var filter = new Dictionary<string, object>
        {
            { primaryKeyName , id },
        };

        var query = _SqlGenerator.Count(tableName, filter);
        var result = await ExecuteScalarAsync<int>(query);

        return result > 0;
    }

    Task<Record> IRecordRepository.GetAsync(string tableName, Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> TryGetAsync(string tableName, Guid id, out Record result)
    {
        throw new NotImplementedException();
    }
}
