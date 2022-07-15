using FluentMigrator.Runner;

using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace Paganod.Sql.DDL.FluentMigrator;

public static class Extensions
{
    public static IMigrationRunnerBuilder AddDbType(this IMigrationRunnerBuilder runnerBuilder, string dbType = "")
    {
        runnerBuilder.ConfigureGlobalProcessorOptions(options =>
        {
            options.PreviewOnly = true;
        });

        return runnerBuilder.AddSQLite();

        //return dbType switch
        //{
        //    Constants.Database.Engine.SqlServer => runnerBuilder.AddSqlServer2016(),
        //    Constants.Database.Engine.MySql => runnerBuilder.AddMySql5(),
        //    Constants.Database.Engine.SqlLite => runnerBuilder.AddSQLite(),

        //    _ => throw new NotImplementedException(),
        //};
    }

    public static void AddDatabaseMigrationRunner(this IMigrationRunnerBuilder rb, string providerName)
    {
        switch (providerName.ToLower())
        {
            case "mysql":
                rb.AddMySql5();
                break;

            case "postgres":
                rb.AddPostgres();
                break;
        }
    }

    public static IServiceCollection AddMyLogging(this IServiceCollection services, StringWriter sw)
    {
        services.AddSingleton(sw);
        //services.AddLogging(lb => lb.AddMyLogger());

        return services;
    }

    //public static ILoggingBuilder AddMyLogger(this ILoggingBuilder loggingBuilder)
    //{
    //    //var logger = new MyLoggerProvider();

    //    loggingBuilder.Services.AddSingleton<ILoggerProvider, MyLoggerProvider>()
    //        .Configure<LogFileFluentMigratorLoggerOptions>(opt =>
    //        {
    //            opt.OutputFileName = "Test.sql";
    //            opt.ShowSql = true;
    //        });

    //    return loggingBuilder;
    //}
}
