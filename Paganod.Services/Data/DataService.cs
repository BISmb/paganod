using System;
using OneOf;
using Paganod.Data.Shared.Interfaces;
using Paganod.Shared.Types;

namespace Paganod.Services;

public interface IDataService
{
    Task<OneOf<Record>> GetRecordAsync(string tableName, Guid recordId); // returns: Record, NotFound, TableDoesNotExist
    Task<OneOf<IEnumerable<Record>>> GetRecordsAsync(string tableName, int pageNumber, int countPerPage);
    Task<int> SaveRecords(string tableName, IEnumerable<Record> records);
    Task<Record> SaveRecord(string taleName, Record record);
    Task<int> DeleteRecord(string tableName, Guid recordId);
}

public class DataService : IDataService
{
    private readonly IAppDbContext appDbContext;

    public DataService(IAppDbContext prmAppDbContext)
    {
        appDbContext = prmAppDbContext;
    }

    public async Task<int> DeleteRecord(string tableName, Guid recordId)
    {
        using var db = await appDbContext.GetUserDatabaseAsync();

        // call pre-event for any plugins, etc.

        var numOfRecordsDeleted = await db.Data.DeleteAsync(tableName, recordId);

        // call post-event for any plugins, etc.

        return numOfRecordsDeleted;
    }

    public async Task<OneOf<Record>> GetRecordAsync(string tableName, Guid recordId)
    {
        using var db = await appDbContext.GetUserDatabaseAsync();

        // call pre-event for any plugins, etc.
        bool error = false;

        //if (error)
        //    return "There was an error";

        var recordToReturn = await db.Data.GetAsync(tableName, recordId);

        // call post-event for any plugins, etc.

        return recordToReturn;
    }

    public async Task<OneOf<IEnumerable<Record>>> GetRecordsAsync(string tableName, int pageNumber, int countPerPage)
    {
        using var db = await appDbContext.GetUserDatabaseAsync();

        var recordsToReturn = await db.Data.GetAsync(tableName, pageNumber, countPerPage);
        return recordsToReturn;
    }

    public async Task<Record> SaveRecord(string tableName, Record record)
    {
        using var db = await appDbContext.GetUserDatabaseAsync();

        // call pre-event for any plugins, etc.

        await db.Data.SaveAsync(tableName, record.Data);

        var primaryKeyName = appDbContext.SchemaModels.GetPrimaryKeyForTable(tableName);
        var recordToReturn = await db.Data.GetAsync(tableName, record.Get<Guid>(primaryKeyName));

        // call post-event for any plugins, etc.

        return recordToReturn;
    }

    public Task<int> SaveRecords(string tableName, IEnumerable<Record> records)
    {
        throw new NotImplementedException();
    }
}

