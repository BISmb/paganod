using Paganod.Data.Shared.Types;
using Paganod.Sql.DDL.FluentMigrator;
using Paganod.Sql.DML;
using Paganod.Sql.Utility;
using Paganod.Types.Domain;
using SqlKata;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Paganod.Sql.DDL.Migrations;

public partial class PaganodMigration
{
    private Task Handle_Column_Add(IAddColumnOperation operation)
    {
        string tableName = operation.TableName.ToLower();
        string colName = operation.Name.ToLower();

        var dbType = Utils.GetDbTypeFromFieldType(operation.PaganodType);
        string strDbType = FluentMigratorMethods.GetDbType(dbType, null, null);
        
        Alter.Table(tableName).AddColumn(colName).AsCustom(strDbType);

        var newSchemaColumn = new SchemaColumn
        {
            SchemaModelId = TargetSchemaModel.Id,
            Name = operation.Name,
            Alias = operation.Alias,
            Type = operation.PaganodType,
        };
        AppDbContext.SchemaColumns.Add(newSchemaColumn);

        // if this column has an alias that matches an existing column, it is a incremented version 2 column
        if (TargetSchemaModel.Columns.Any(x => x.Name.Equals(operation.Alias, StringComparison.OrdinalIgnoreCase)))
        {
            // this column is "v2"
            newSchemaColumn.Version = 2;

            // deprecate current "matching" column
            var currentColumn = AppDbContext.SchemaColumns.GetByColumnName(newSchemaColumn.Alias);
            currentColumn.Version = 1;

            // v1 data copy to v2 column (rename, no transformation required)
            UpdateVersionedColumns(tableName, newSchemaColumn.Name, currentColumn.Name);

            // v1 data copy to v2 column (type change, transformation required)
            //UpdateVersionedColumns(tableName, newSchemaColumn.Name, currentColumn.Name, "");
        }

        return Task.CompletedTask;
    }

    private void UpdateVersionedColumns(string tableName, string newColumnName, string currentColumnName, string transformationFunction = "")
    {
        if (string.IsNullOrWhiteSpace(transformationFunction))
            currentColumnName = AddTransformationFunctionToColum(currentColumnName, transformationFunction);

        var query = new Query(tableName).AsUpdate(newColumnName, currentColumnName);
        var sqlGenerator = SqlKataGenerator.GetKataCompiler(DbType); 
        var generatedQuery = sqlGenerator.Compile(query);

        Execute.Sql(generatedQuery.Sql);
    }

    private string AddTransformationFunctionToColum(string newColumnName, string transformationFunction)
    {
        // this function is not implemented

        return newColumnName;
    }
}