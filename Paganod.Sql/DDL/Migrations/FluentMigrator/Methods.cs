using FluentMigrator.Infrastructure;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Generators.MySql;

using Microsoft.Extensions.DependencyInjection;

using Paganod.Config.Core.Sql._FluentMigrator;

using System;
using System.Data;

namespace Paganod.Sql.DDL.FluentMigrator;

public static class FluentMigratorMethods
{
    public static Func<DbType, int?, int?, string> GetDbType;

    public static IMigrationRunner GetMigrationRunner(IDbConnection dbConnection, bool preview = false)
    {
        var serviceCollection = new ServiceCollection()
            .AddFluentMigratorCore()
            .AddScoped<MySqlQuoter, PaganodMySqlQuoter>()
            //.AddScoped<SQLiteQuoter, _SqlLiteQuoter>()
            .AddLogging(log => log.AddFluentMigratorConsole());

        serviceCollection.ConfigureRunner(runner =>
        {
            runner.AddDbType();
            runner.WithGlobalConnectionString(dbConnection.ConnectionString);
            runner.AsGlobalPreview(preview);
        });

        var serviceProvider = serviceCollection.BuildServiceProvider();
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

        //SetTypeMapper(runner);

        if (GetDbType is null)
            GetDbType = (dbType, size, precision) => GetDbTypeInternal(runner.Processor.DatabaseType, dbType, size, precision);

        return runner;
    }

    private static string GetDbTypeInternal(string databaseEngine, DbType type, int? size, int? precision)
    {
        if (type == DbType.Guid)
        {
            type = DbType.String;
            size = 36;
        }

        return type switch
        {
            DbType.Date => "datetime",
            DbType.DateTime => "datetime",
            DbType.DateTime2 => "datetime",
            DbType.DateTimeOffset => "datetime",

            DbType.Decimal => $"decimal({size}, {precision})",
            DbType.Double => $"decimal({size}, {precision})",
            DbType.Int16 => "smallint",
            DbType.Int32 => "int",
            DbType.Int64 => "bigint",

            _ => $"char({size ?? 120})",
        };
    }


    //private static Func<DbType, int?, int?, string> SetTypeMapper(IMigrationRunner runner)
    //{
    //    var availableFields = runner.Processor.GetType().GetRuntimeFields();

    //    foreach (var c in availableFields)
    //    {
    //        var myVal = c.GetValue(runner.Processor);

    //        if (c.Name == nameof(IGeneratorAccessor.Generator))
    //        {
    //            foreach (var f in myVal.GetType().GetRuntimeProperties())
    //            {
    //                var myVal2 = f.GetValue(myVal);

    //                if (typeof(IColumn).IsAssignableFrom(myVal2.GetType()))
    //                {
    //                    var fields = myVal2.GetType().GetRuntimeFields();
    //                    var properties = myVal2.GetType().GetProperties();
    //                    var members = myVal2.GetType().GetMembers();
    //                    var interfaces = myVal2.GetType().GetCustomAttributes();
    //                    var k = myVal2.GetType().GetRuntimeMethods();

    //                    foreach (var method in myVal2.GetType().GetRuntimeMethods())
    //                    {
    //                        if (method.Name == "GetTypeMap")
    //                        {
    //                            //GetTypeMap = method;

    //                            //GetDbType = new Func<DbType, int?, int?, string>((type, size, precision) => (string)method.Invoke(myVal2, new object[] { type, size, precision }));
    //                            //// Func<DbType, int?, int?, string>
    //                            return new Func<DbType, int?, int?, string>((type, size, precision) =>
    //                            {
    //                                if (type == DbType.Guid)
    //                                {
    //                                    type = DbType.String;
    //                                    size = 36;
    //                                }

    //                                string dbType = type switch
    //                                {
    //                                    DbType.Date => "datetime",
    //                                    DbType.DateTime => "datetime",
    //                                    DbType.DateTime2 => "datetime",
    //                                    DbType.DateTimeOffset => "datetime",

    //                                    DbType.Decimal => "decimal(13, 2)",
    //                                    DbType.Double => "decimal(13, 10)",
    //                                    DbType.Int16 => "smallint",
    //                                    DbType.Int32 => "int",
    //                                    DbType.Int64 => "bigint",

    //                                    _ => $"char({ size ?? 120 })",
    //                                };

    //                                var strDbType = (string)method.Invoke(myVal2, new object[] { type, size, precision });
    //                                return strDbType;
    //                            });

    //                            //var typeMap = method.Invoke(myVal2, new object[] { DbType.Decimal, 5, 6 });
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }

    //    throw new NotImplementedException();
    //}
}
