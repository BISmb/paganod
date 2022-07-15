using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Paganod.Data.App.Internal;
using Paganod.Data.Contexts.App;
using Paganod.Data.Shared.Interfaces;
using Paganod.Types.Base.Paganod;
using System.Data;
using System.Data.Common;

namespace Paganod.Data.Tests;

[SingleThreaded]
internal class TableIntegrationTests : DataTest
{
    private Task<string> CreateTableAsync()
    {
        var tblInfo = (TableName: "transactions", PrimaryKeyName: "transaction_id", PrimaryKeyType: DbType.Guid);
        return CreateTableAsync(tblInfo);
    }

    private async Task<string> CreateTableAsync((string TableName, string PrimaryKeyName, DbType PrimaryKeyType) tblInfo)
    {
        var createMigrations = await _appDb.Schema.Configure.CreateTable(tblInfo.TableName, tblInfo.PrimaryKeyName, tblInfo.PrimaryKeyType)
                                                                    .GenerateMigrationsAsync();

        await _paganodDb.ExecuteMigrationOnTargetAsync(createMigrations.ForwardMigration, _appDb);

        return tblInfo.TableName;
    }

    [Test]
    public async Task Can_Create_Table()
    {
        var tblInfo = (TableName: "transactions", PrimaryKeyName: "transaction_id", PrimaryKeyType: DbType.Guid);

        if (!_appDb.Schema.Read.ContainsTable(tblInfo.TableName))
            await CreateTableAsync(tblInfo);

        // assertions
        _paganodDb.SchemaModels.Exists(tblInfo.TableName);
        _paganodDb.SchemaColumns.Exists(tblInfo.TableName, tblInfo.PrimaryKeyName);

        _appDb.Schema.Read.ContainsTable(tblInfo.TableName).Should().BeTrue();
        _appDb.Schema.Read.GetPrimaryKeyNameForTable(tblInfo.TableName).Should().Be(tblInfo.PrimaryKeyName);
    }

    [Test]
    public async Task Can_Add_Column_ToTable()
    {
        string tableName = await CreateTableAsync();

        var colInfo = (Name: "notes", Type: FormFieldType.Text);

        var addMigrations = await _appDb.Schema.Configure.AddColumn(colInfo.Name, colInfo.Type, alternativeTableName: tableName)
                                                           .GenerateMigrationsAsync();

        var migration = addMigrations.ForwardMigration;
        migration.TargetSchemaModelId = _paganodDb.SchemaModels.GetByTableName(tableName).Id;
        await _paganodDb.ExecuteMigrationOnTargetAsync(migration, _appDb);

        // assertions
        _paganodDb.SchemaColumns.Exists(tableName, colInfo.Name).Should().BeTrue();
        _paganodDb.SchemaColumns.GetByTableNameAndColumnName(tableName, colInfo.Name).Type.Should().Be(colInfo.Type);
        _appDb.Schema.Read.ContainsColumn(tableName, colInfo.Name).Should().BeTrue();
    }
}