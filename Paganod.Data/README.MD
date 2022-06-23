Paganod Db Context is the db context that every tenant's database will use.
Tenants Db Context is the paganod system context.

https://www.entityframeworktutorial.net/efcore/cli-commands-for-ef-core-migration.aspx#migrations-add

To generate new migrations:

1. Change working directory to Paganod.Data.Migrations
2. Run: > dotnet ef migrations add InitialCreate --context AppDbContext --output-dir Migrations/App

To apply migrations:

> dotnet ef database update

When new databases are initalized, make sure the below is run so the MigrationHistory is caught up

> dotnet ef migrations script --idempotent