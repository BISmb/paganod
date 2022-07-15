using Paganod.Data.Shared.Interfaces;
using Paganod.Data.Shared.Types;
using Paganod.Shared;
using Paganod.Shared.Logic;
using Paganod.Types.Base.Paganod;
using Paganod.Types.Base.Paganod.Schema;
using Paganod.Types.Domain;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paganod.Data.Records.Schema;

internal partial class SchemaConfigurator
{
    /// <summary>
    /// Get the primary key name for a given tablename
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    private string GeneratePrimaryKeyNameFromTableName(string tableName)
    {
        return "transaction_id"; // todo: call Humanizer.Singular()
    }

    /// <summary>
    /// When creating a new table, add the default columns
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void AddDefaultColumns(string tableName)
    {
        AddColumn("CreatedOn", FormFieldType.DateTime, alternativeTableName: tableName);
        AddColumn("ModifiedOn", FormFieldType.DateTime, alternativeTableName: tableName);

        // Created By
        // Modified By
        // Changed By
    }

    /// <summary>
    /// Specify a CreateTableOperation as part of the generated migration
    /// </summary>
    /// <param name="tableName">The tableName to create</param>
    public ISchemaConfigurator CreateTable(string tableName, string primaryKeyName = null, DbType? primaryKeyType = null)
    {
        var operation = new CreateTableOperation
        {
            TableName = tableName,
            PrimaryKeyName = primaryKeyName ?? GeneratePrimaryKeyNameFromTableName(tableName),
            PrimaryKeyType = primaryKeyType ?? DbType.Guid,
        };
        string jsonData = Common.SerializeToJson(operation);
        
        var migrationOperation = new SchemaMigrationOperation(SchemaMigrationOperationType.CreateTable, jsonData);
        SqlOperations.Add(migrationOperation);

        AddDefaultColumns(tableName);
        return new SchemaConfigurator(Db) { _TargetTable = tableName, SqlOperations = this.SqlOperations };
    }

    /// <summary>
    /// Specify a RenameTableOperation as part of the generated migration
    /// </summary>
    /// <param name="tableName">The tableName to create</param>
    public ISchemaConfigurator RenameTable(string tableName, string newTableName)
    {
        var operation = new RenameTableOperation
        {
            CurrentTableName = tableName,
            NewTableName = newTableName,
        };
        string jsonData = Common.SerializeToJson(operation);

        var migrationOperation = new SchemaMigrationOperation(SchemaMigrationOperationType.RenameTable, jsonData);
        SqlOperations.Add(migrationOperation);

        return this;
    }

    /// <summary>
    /// Specify a DeleteTableOperation as part of the generated migration
    /// </summary>
    /// <param name="tableName">The tableName to create</param>
    public ISchemaConfigurator RemoveTable(string tableName)
    {
        var operation = new DeleteTableOperation
        {
            TableName = tableName,
        };
        string jsonData = Common.SerializeToJson(operation);

        var migrationOperation = new SchemaMigrationOperation(SchemaMigrationOperationType.DeleteTable, jsonData);
        SqlOperations.Add(migrationOperation);

        return this;
    }
}
