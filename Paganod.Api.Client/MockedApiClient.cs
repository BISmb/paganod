using System;
using Paganod.Api.Shared;
using Paganod.Api.Shared.Feature.Config.Schema.Responses;
using Paganod.Shared.Types;
using Paganod.Types.Base.Paganod;

namespace Paganod.Api.Client;

public class MockedApiClient : IPaganodApiClient
{
    static Dictionary<string, List<Record>> MockedRecords = new Dictionary<string, List<Record>>(StringComparer.OrdinalIgnoreCase)
    {
        ["transactions"] = new List<Record>
        {
            new Record("transactions")
            {
                ["transaction_id"] = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                ["columnA"] = "This is column A (text)",
                ["columnB"] = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                ["columnC"] = "Check",
            },
        },

        ["accounts"] = new List<Record>
        {
            new Record("accounts")
            {
                ["account_id"] = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                ["name"] = "Chase Bank",
            },
        },
    };

    static Dictionary<string, SchemaModelDto> MockedSchemas = new Dictionary<string, SchemaModelDto>(StringComparer.OrdinalIgnoreCase)
    {
        ["transactions"] = new SchemaModelDto
        {
            PrimaryKeyName = "transaction_id",
            PrimaryKeyType = System.Data.DbType.Guid,
            SchemaModelId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            TableName = "transactions",
            Columns = new SchemaColumnDto[]
            {
                new SchemaColumnDto("columnA", FormFieldType.Text),
                new SchemaColumnDto("columnB", FormFieldType.Reference, options: ("ReferencedTable", "Accounts")),
                new SchemaColumnDto("columnC", FormFieldType.Dropdown, options: ("DropdownOptions", "Credit,Debit,Check,Transfer")),
            },
        },

        ["accounts"] = new SchemaModelDto
        {
            PrimaryKeyName = "account_id",
            PrimaryKeyType = System.Data.DbType.Guid,
            SchemaModelId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            TableName = "accounts",
            Columns = new SchemaColumnDto[]
            {
                new SchemaColumnDto("name", Types.Base.Paganod.FormFieldType.Text),
            },
        },
    };


    public MockedApiClient()
    {
    }

    public async Task<Record[]> GetAsync(string tableName, int pageNumber, int countPerPage)
    {
        await Task.Delay(1000);
        return MockedRecords[tableName].ToArray();
    }

    public async Task<Record?> GetAsync(string tableName, Guid recordId)
    {
        await Task.Delay(1000);

        // get id column of record
        var idColumnName = MockedSchemas[tableName].PrimaryKeyName;

        // return record with that Idz

        return MockedRecords[tableName].FirstOrDefault(x => x.Get<Guid>(idColumnName) == recordId);
    }

    public async Task<SchemaModelDto> GetSchemaModelAsync(string tableName)
    {
        await Task.Delay(1000);
        return MockedSchemas[tableName];
    }

    public async Task<SchemaModelDto> GetSchemaModelAsync(Guid schemaId)
    {
        await Task.Delay(1000);
        return MockedSchemas.First(x => x.Value.SchemaModelId == schemaId).Value;
    }

    public async Task<SchemaModelDto[]> GetSchemaModels()
    {
        await Task.Delay(1000);
        return MockedSchemas.Values.ToArray();
    }

    public async Task<Record> SaveAsync(Record record)
    {
        await Task.Delay(1000);

        var primaryKeyName = MockedSchemas[record.TableName].PrimaryKeyName;

        if (!record.Data.ContainsKey(primaryKeyName))
            record.Data.Add(primaryKeyName, Guid.NewGuid());

        if(!record.Data.ContainsKey("CreatedOn"))
            record.Data.Add("CreatedOn", DateTime.Now);

        MockedRecords[record.TableName].Add(record);
        return record;
    }
}

