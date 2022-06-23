using Paganod.Data.Shared.Types;
using Paganod.Types.Base.Paganod.Schema;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paganod.Shared;
using Paganod.Types.Domain;
using Paganod.Data.Shared.Interfaces;
using Paganod.Types.Base.Paganod;
using Paganod.Shared.Logic;

namespace Paganod.Data.App.Schema;

internal partial class SchemaConfigurator
{
    // <summary>
    /// Specify an AddColumnOperation for the generated migration
    /// </summary>
    /// <param name="tableName">The tableName to create</param>
    public ISchemaConfigurator AddColumn(string colName, FormFieldType fieldType, bool isRequired = false, object defaultValue = null, Dictionary<string, string> options = null, string alias = null, string alternativeTableName = null)
    {
        var operation = new AddColumnOperation
        {
            TableName = alternativeTableName ?? _TargetTable,
            Name = colName,
            PaganodType = fieldType,
            Alias = alias ?? colName,
            Options = options ?? new Dictionary<string, string>(),
        };
        string jsonData = Common.SerializeToJson(operation);

        var migrationOperation = new SchemaMigrationOperation(SchemaMigrationOperationType.AddColumn, jsonData);
        SqlOperations.Add(migrationOperation);

        return this;
    }

    // <summary>
    /// Specify an AddColumnOperation for the generated migration
    /// </summary>
    /// <param name="tableName">The tableName to create</param>
    public ISchemaConfigurator RemoveColumn(string colName)
    {
        var operation = new AddColumnOperation
        {
            TableName = _TargetTable,
            Name = colName,
        };
        string jsonData = Common.SerializeToJson(operation);

        var migrationOperation = new SchemaMigrationOperation(SchemaMigrationOperationType.DeleteColumn, jsonData);
        SqlOperations.Add(migrationOperation);

        return this;
    }

    public ISchemaConfigurator AlterColumn(string columnName, FormFieldType fieldType)
    {
        RemoveColumn(columnName);
        AddColumn(columnName, fieldType);
        // Make a transform data Operation here? (ColumnA data tranfrom to Column B data)

        return this;
    }

    public ISchemaConfigurator RenameColumn(string columnName, string newColumnName)
    {
        //if (_IsTargetSchemaVersioned)
        //    return RenameColumnForVersionControlledTable(columnName, newColumnName);
        //else
            return RenameColumnInTable(columnName, newColumnName);
    }

    public ISchemaConfigurator AlterColumn(string columnName, 
                                           FormFieldType newType,
                                           string sqlTransformationFunction,
                                           bool isRequired = false, 
                                           object defaultValue = null, 
                                           Dictionary<string, string> options = null)
    {
        var operation = new AlterColumnOperation
        {
            TableName = _TargetTable,
            Name = columnName,
            PaganodType = newType,
            Options = options ?? new Dictionary<string, string>(),
            TransformationExpression = sqlTransformationFunction,
            // todo: remember to add a transformation function to the MigrationOperation
        };
        string jsonData = Common.SerializeToJson(operation);

        var migrationOperation = new SchemaMigrationOperation(SchemaMigrationOperationType.AlterColumn, jsonData);
        SqlOperations.Add(migrationOperation);

        return this;
    }

    //private struct AddNewColumnParams // cut down on some of the functions that take many parameters
    //{
    //    string tableName;
    //    string columnName;
    //    FormFieldType newType;
    //    bool isRequired = false;
    //    object defaultValue = null;
    //    Dictionary<string, string> options = null;
    //    string alias = null;
    //}

    private ISchemaConfigurator RenameColumnInTable(string columnName, string newColumnName)
    {
        var operation = new RenameColumnOperation
        {
            TableName = _TargetTable,
            CurrentColumnName = columnName,
            NewColumnName = newColumnName,
        };
        string jsonData = Common.SerializeToJson(operation);

        var migrationOperation = new SchemaMigrationOperation(SchemaMigrationOperationType.RenameTable, jsonData);
        SqlOperations.Add(migrationOperation);

        return this;
    }

    private ISchemaConfigurator RenameColumnForVersionControlledTable(string columnName, string newColumnName)
    {
        var targetColumn = Db.SchemaColumns.GetByTableNameAndColumnName(_TargetTable, columnName);
        RemoveColumn(columnName);
        AddColumn(targetColumn.Name, targetColumn.Type); // todo: fill out more here
                                                         // Make a copy data operation here? (ColumnA data copy to Column B data)
        return this;
    }

}
