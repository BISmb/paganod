using System;
namespace Paganod.Shared.Constants;

public static class ApiRoutes
{
    public const string Home = "/api";

    public static class Config
    {
        public const string Home = $"{ApiRoutes.Home}/config";

        public const string New = "/new";
        public const string GetBySchemaId = $"get/{{{Args.SchemaId}}}";
        public const string GetByTableName = $"get/{{{Args.SchemaId}}}";
        public const string Alter = "alter";
        public const string Delete = "delete";

        public static class Args
        {
            public const string TableName = "tableName";
            public const string SchemaId = "schemaId";
        }
    }

    public static class Data
    {
        public const string Home = $"{ApiRoutes.Home}/data";

        public const string GetMany = $"{{{Args.TableName}}}/{{{Args.PageNumber}}}/{{{Args.ResultsPerPage}}}";
        public const string Get = $"{{{Args.TableName}}}/{{{Args.Id}}}";

        //public const string GetBySchemaId = $"/get/{{{Args.SchemaId}}}";
        //public const string GetByTableName = $"/get/{{{Args.SchemaId}}}";
        public const string Alter = "alter";
        public const string Delete = "delete";

        public static class Args
        {
            public const string TableName = "tableName";
            public const string Id = "recordId";
            public const string PageNumber = "pageNumber";
            public const string ResultsPerPage = "pageResults";
        }
    }
}

