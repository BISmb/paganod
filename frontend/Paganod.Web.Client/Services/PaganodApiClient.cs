using Paganod.Api.Shared.Feature.Config.Schema.Commands;
using Paganod.Types.Base.Paganod;
using Paganod.Web.Client.Types;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Paganod.Web.Client.Services;

public interface IPaganodApiClient
{
    Task<Record[]> GetAsync(string tableName, int pageNumber, int countPerPage);
    Task<Record> GetAsync(string tableName, Guid recordId);
    Task<SchemaModelDto> GetSchemaModelAsync(string tableName);
    Task<SchemaModelDto> GetSchemaModelAsync(Guid schemaId);
    Task<IEnumerable<Attachment>> GetAttachmentsAsync(string tableName, Guid recordId);
    Task UploadAttachmentAsync(NewAttachment attachment);
    Task<Guid?> CreatSchemaAsync(CreateSchemaCommand model);
}

public class TestPaganodApiClient : IPaganodApiClient
{
    private readonly HttpClient Client; 

    public TestPaganodApiClient(IHttpClientFactory httpClientFactory)
    {
        Client = httpClientFactory.CreateClient("Paganod");
    }

    public async Task<Guid?> CreatSchemaAsync(CreateSchemaCommand model)
    {
        var response = await Client.PostAsJsonAsync<CreateSchemaCommand>("config/new", model);

        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<Guid>()
            : null; 
    }

    public async Task<Record[]> GetAsync(string tableName, int pageNumber, int countPerPage)
    {
        try
        {
            var records = new Record[countPerPage];

            tableName = tableName.ToLower();

            // todo: need to put in custom json serializer
            var rows = await Client.GetFromJsonAsync<IDictionary<string, object>[]>($"data/{tableName}/{pageNumber}/{countPerPage}");

            for (int i = 0; i < rows.Length; i++)
                records[i] = new Record(tableName, rows[i]);

            return records;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Client Side Exception caught");
            return new Record[]{ };
        }
    }

    public async Task<Record> GetAsync(string tableName, Guid recordId)
    {
        try
        {
            tableName = tableName.ToLower();

            // todo: need to put in custom json serializer
            var row = await Client.GetFromJsonAsync<IDictionary<string, object>>($"data/{tableName}/{recordId}");
            return new Record(tableName, row);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Client Side Exception caught");
            return null;
        }
    }

    public async Task<IEnumerable<Attachment>> GetAttachmentsAsync(string tableName, Guid recordId)
    {
        await Task.Delay(0);

        return new Attachment[]
        {
            new Attachment
            {
                AttatchmentId = Guid.NewGuid(),
                FileId = Guid.NewGuid(),
                FileType = "pdf",
                Name = "TransactionExport.pdf"
            }
        };
    }

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

    public async Task UploadAttachmentAsync(NewAttachment attachment)
    {
        Console.WriteLine(attachment.FileContent is null);

        using var content = new MultipartFormDataContent();

        attachment.FileContent.Headers.ContentType = new MediaTypeHeaderValue(attachment.FileType);

        //content.Add(content: attachment.FileContent, name: "\"file\"", fileName: attachment.Name);

        // using (var http = new HttpClient())
        //     await http.PostAsync("/files/upload", content);

        await Task.CompletedTask;

        Console.WriteLine("Leaving PaganodApiClient Method");
        content.Dispose();
    }
}