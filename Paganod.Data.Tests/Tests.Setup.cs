using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Paganod.Data.App.Internal;
using Paganod.Data.Contexts.App;
using Paganod.Data.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paganod.Data.Tests;

#nullable disable

internal class DataTest
{
    protected IAppDbContext _paganodDb;
    //protected IAppDbConnection _appDb;

    [SetUp]
    public async Task SetupAsync()
    {
        await SetupPaganodDatabase();
        await SetupAppDatabase();
    }

    [TearDown]
    public void Teardown()
    {
        File.Delete("paganod.db");
        File.Delete("app.db");
    }

    private async Task SetupPaganodDatabase()
    {
        string databaseName = "paganod.db";

        if (File.Exists(databaseName))
            File.Delete(databaseName);

        string paganodConnectionString = GetPaganodConnectionString(databaseName);

        var dbOptions = new DbContextOptionsBuilder<EfDataAccess>()
                                        .UseSqlite(paganodConnectionString)
                                        .Options;

        _paganodDb = new AppDbContext(dbOptions);
        await _paganodDb.InitalizeAsync();
    }

    private Task SetupAppDatabase()
    {
        string databaseName = "app.db";

        if (File.Exists(databaseName))
            File.Delete(databaseName);

        string appConnectionString = GetAppConnectionString(databaseName);

        _appDb = new AppBDbContext(_paganodDb, (typeof(SqliteConnection), GetAppConnectionString(databaseName)));

        // ensure sqlite database is created
        using (var dbConnection = _appDb.NewConnection())
        {
            dbConnection.Open();
            dbConnection.Close();
        }

        return Task.CompletedTask;
    }

    private string GetAppConnectionString(string databaseName)
    {
        return $"Data Source={databaseName}";
    }

    private string GetPaganodConnectionString(string databaseName)
    {
        return $"Data Source={databaseName}";
    }
}
