using System.Net.Http.Json;
using Paganod.Api.Shared;
using Paganod.Api.Shared.Feature.Config.Schema.Responses;
using Paganod.Shared.Types;
using Paganod.Types.Api.Config.Commands;
using Paganod.Types.Base.Paganod;

namespace Paganod.Api.Client;

public class PaganodApiClient : IPaganodApiClient
{
    private readonly string _BaseUrl;
    private HttpClient Client
    {
        get => GetNewHttpClient();
    }

    public PaganodApiClient(string prmBaseUrl)
    {
        _BaseUrl = prmBaseUrl;
    }

    internal virtual HttpClient GetNewHttpClient()
    {
        return new HttpClient()
        {
            BaseAddress = new Uri(_BaseUrl),
        };
    }

    public async Task<Guid?> CreatSchemaAsync(CreateSchemaCommand model)
    {
        var response = await Client.PostAsJsonAsync("config/new", model);

        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<Guid>()
            : null;
    }

    public async Task<Record[]> GetAsync(string tableName, int pageNumber, int countPerPage)
    {
        var records = new Record[countPerPage];

        tableName = tableName.ToLower();

        // todo: need to put in custom json serializer
        var rows = await Client.GetFromJsonAsync<IDictionary<string, object>[]>($"data/{tableName}/{pageNumber}/{countPerPage}");

        for (int i = 0; i < rows.Length; i++)
            records[i] = new Record(tableName, rows[i]);

        return records;
    }

    public async Task<Record?> GetAsync(string tableName, Guid recordId)
    {
        tableName = tableName.ToLower();

        // todo: need to put in custom json serializer
        var row = await Client.GetFromJsonAsync<IDictionary<string, object>>($"data/{tableName}/{recordId}");
        return new Record(tableName, row!);

    }

    //public async Task<IEnumerable<Attachment>> GetAttachmentsAsync(string tableName, Guid recordId)
    //{
    //    await Task.Delay(0);

    //    return new Attachment[]
    //    {
    //        new Attachment
    //        {
    //            AttatchmentId = Guid.NewGuid(),
    //            FileId = Guid.NewGuid(),
    //            FileType = "pdf",
    //            Name = "TransactionExport.pdf"
    //        }
    //    };
    //}

    public Task<SchemaModelDto> GetSchemaModelAsync(string tableName)
    {
        var schemaModel = new SchemaModelDto
        {
            TableName = "transactions",
            PrimaryKeyName = "transaction_id",
            SolutionId = Guid.Empty,
            PrimaryKeyType = System.Data.DbType.Guid,
            Columns = new SchemaColumnDto[]
            {
                new SchemaColumnDto("columnA", FormFieldType.Text),
                new SchemaColumnDto("columnB", FormFieldType.Reference, options: new (string, string)[] {
                    ("ReferencedTable", "Accounts"),
                }),
                new SchemaColumnDto("columnC", FormFieldType.Dropdown, options: new (string, string)[] {
                    ("DropdownOptions", "Credit,Debit,Check,Transfer"),
                }),
            },
        };

        return Task.FromResult(schemaModel);
    }

    public async Task<SchemaModelDto> GetSchemaModelAsync(Guid schemaId)
    {
        var response = await Client.GetFromJsonAsync<SchemaModelDto>($"metadata/schema/{schemaId}");
        return response;
    }

    public async Task<SchemaModelDto[]> GetSchemaModels()
    {
        await Task.Delay(0);

        return new SchemaModelDto[]
        {
            new SchemaModelDto
            {
                SchemaModelId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                TableName = "transactions",
                PrimaryKeyName = "transaction_id",
                Columns = new SchemaColumnDto[]
                {
                    new SchemaColumnDto("columnA", FormFieldType.Date),
                },
            }
        };
    }

    public Task<Record> SaveAsync(Record record)
    {
        throw new NotImplementedException();
    }

    //public async Task UploadAttachmentAsync(NewAttachment attachment)
    //{
    //    Console.WriteLine(attachment.FileContent is null);

    //    using var content = new MultipartFormDataContent();

    //    attachment.FileContent.Headers.ContentType = new MediaTypeHeaderValue(attachment.FileType);

    //    //content.Add(content: attachment.FileContent, name: "\"file\"", fileName: attachment.Name);

    //    // using (var http = new HttpClient())
    //    //     await http.PostAsync("/files/upload", content);

    //    await Task.CompletedTask;

    //    Console.WriteLine("Leaving PaganodApiClient Method");
    //    content.Dispose();
    //}
}
