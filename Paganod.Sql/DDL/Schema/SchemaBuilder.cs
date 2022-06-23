namespace Paganod.Sql.DDL.Schema;

/*
// top-level exposed class
public interface IMigrationService
{
    Task<(Migration ForwardMigration, Migration? SunsetMigration)> GenerateMigrationsAsync(CreateSchemaCommand createSchemaCommand);
    Task<(Migration ForwardMigration, Migration? SunsetMigration)> GenerateMigrationsAsync(AlterSchemaCommand alterSchemaCommand);
    Task<(Migration ForwardMigration, Migration? SunsetMigration)> ApplyMigrationAsync(Migration moigration);
}

// top-level exposed class
public static class SchemaBuilder
{
    public static TableBuilder NewTable(string tableName, string primaryKeyName, DbType primaryKeyType)
    {
        return new TableBuilder(tableName, primaryKeyName, primaryKeyType);
    }

    public static TableBuilder Table(string tableName)
    {
        return new TableBuilder(tableName);
    }

    public static async Task<(Migration ForwardMigration, Migration? SunsetMigration)> GenerateMigrationsAsync(IAppDbContext db, CreateSchemaCommand createSchemaCommand, AlterSchemaCommand alterSchemaCommand)
    {
        // call config service code (providing AppDbContext) to generate a migrartion from the alterschemacommand
        var cfgService = new MigrationsGenerator(db);

        bool doesTableExistInDatabase = false;

        return doesTableExistInDatabase
            ? await cfgService.GenerateCreateSchemaMigration(createSchemaCommand)
            : await cfgService.GenerateAlterMigrationsAsync(alterSchemaCommand);
    }

    public static async Task ApplyChangesAsync(IAppDbContext db, CreateSchemaCommand createSchemaCommand, AlterSchemaCommand alterSchemaCommand)
    {
        var migrations = await GenerateMigrationsAsync(db, createSchemaCommand, alterSchemaCommand);

        // call db to run migration (create schema if table does not exist, alter if the table exists)

        await Task.CompletedTask;
    }
}

public interface ISchemaConfigurator
{
    Task<(Migration ForwardMigration, Migration? SunsetMigration)> ToMigrationsAsync(IAppDbContext db);
    Task ApplyChangesAsync(IAppDbContext db);
}

public class TableBuilder : ISchemaConfigurator
{

    private CreateSchemaCommand createSchemaCommand { get; }
    private AlterSchemaCommand alterSchemaCommand { get; }

    public TableBuilder()
    {
        createSchemaCommand = new CreateSchemaCommand();
        alterSchemaCommand = new AlterSchemaCommand();
    }


    public TableBuilder(string tableName, string primaryKeyName, DbType primaryKeyType)
        : this()
    {
        alterSchemaCommand.TableName = tableName;
        createSchemaCommand.TableName = tableName;
        createSchemaCommand.PrimaryKeyName = primaryKeyName;
        createSchemaCommand.PrimaryKeyType = primaryKeyType;
    }

    public TableBuilder(string tableName)
        : this()
    {
        alterSchemaCommand.TableName = tableName;
        createSchemaCommand.TableName = tableName;
    }

    public TableBuilder(CreateSchemaCommand createSchemaCommand, AlterSchemaCommand alterSchemaCommand)
        : this()
    {
        this.createSchemaCommand = createSchemaCommand;
        this.alterSchemaCommand = alterSchemaCommand;
    }

    public ColumnBuilder WithColumn(string columnName, Shared.Enums.Design.FieldType columnType)
    {
        createSchemaCommand.AddColumn(columnName, columnType);
        alterSchemaCommand.AddColumn(columnName, columnType);
        return new ColumnBuilder(createSchemaCommand, alterSchemaCommand, columnName);
    }

    public TableBuilder AlterColumn(string columnName, Shared.Enums.Design.FieldType columnType)
    {
        alterSchemaCommand.ModifiedSchemaColumns.Add(new AlteredColumnDto { Name = columnName, Type = columnType });
    }

    public TableBuilder AlterColumn(string columnName, Shared.Enums.Design.FieldType columnType)
    {
        alterSchemaCommand.ModifiedSchemaColumns.Add(new AlteredColumnDto { Name = columnName, Type = columnType }); // this column should be the same name as what is in the database
    }

    public TableBuilder RenameColumn(string columnName, string newColumnName)
    {
        alterSchemaCommand.ModifiedSchemaColumns.Add(new AlteredColumnDto { Name = columnName,  }); // this column should be the same name as what is in the database
    }

    //.RenameColumn("oldName", "NewName")
    //        .AlterColumn("", newType)
    //        .DeleteColumn("")

    public Task<(Migration ForwardMigration, Migration? SunsetMigration)> ToMigrationsAsync(IAppDbContext db)
    {
        return SchemaBuilder.GenerateMigrationsAsync(db, createSchemaCommand, alterSchemaCommand);
    }

    public async Task ApplyChangesAsync(IAppDbContext db)
    {
        var migrations = ToMigrationsAsync(db);

        // save migrations to IAppDbContext

        // run paganod migration
        await Task.Delay(1000);
    }
}

public class ColumnBuilder : ISchemaConfigurator
{
    private readonly CreateSchemaCommand createSchemaCommand;
    private readonly AlterSchemaCommand alterSchemaCommand;
    private readonly string columnName;

    public ColumnBuilder(CreateSchemaCommand createSchemaCommand, AlterSchemaCommand alterSchemaCommand, string columnName)
    {
        this.createSchemaCommand = createSchemaCommand;
    }

    public TableBuilder WithOptions(params (string Name, string Value)[] options)
    {
        var targetColumn = createSchemaCommand.Columns.First(x => x.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase));
        targetColumn.Options = options.ToDictionary(x => x.Name, x => x.Value, StringComparer.OrdinalIgnoreCase);
        return new TableBuilder(createSchemaCommand, alterSchemaCommand);
    }

    public ColumnBuilder WithColumn(string columnName, Shared.Enums.Design.FieldType columnType)
    {
        createSchemaCommand.AddColumn(columnName, columnType);
        alterSchemaCommand.AddColumn(columnName, columnType);
        return new ColumnBuilder(createSchemaCommand, alterSchemaCommand, columnName);
    }

    public Task<(Migration ForwardMigration, Migration? SunsetMigration)> ToMigrationsAsync(IAppDbContext db)
    {
        return SchemaBuilder.GenerateMigrationsAsync(db, createSchemaCommand, alterSchemaCommand);
    }

    public Task ApplyChangesAsync(IAppDbContext db)
    {
        var migrations = ToMigrationsAsync(db);
        return SchemaBuilder.ApplyChangesAsync(db, createSchemaCommand, alterSchemaCommand);
    }
}

*/