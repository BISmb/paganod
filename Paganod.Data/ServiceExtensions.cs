using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Paganod.Data.App;
using Paganod.Data.App.Internal;
using Paganod.Data.Contexts.App;
using Paganod.Data.Shared.Interfaces;

namespace Paganod.Data
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigurePaganodDatabaseLayer(this IServiceCollection services, IConfiguration config)
        {
            //services.AddScoped<IAppDbContext>(services =>
            //{
            //    //if (services.GetService<ITenancyResolver>() is not null)
            //    //{
            //    //    // multi tenancy is used
            //    //    var tenantResolver = services.GetRequiredService<ITenancyResolver>();
            //    //    return tenantResolver.GetDataContext();
            //    //}
            //    //else
            //    //{
            //        // multi tenancy is not used
            //        string excelFilePath = @"C:\Users\marty\Desktop\myData.xlsx";
            //        return new ExcelAppDbContext(excelFilePath);
            //    //}
            //});

            // debug database state is not saved
            services.AddScoped<IAppDbContext>((services) =>
            {
                var dbOptions = new DbContextOptionsBuilder<EfDataAccess>()
                                        .UseSqlite("Data Source=/Users/marty/Desktop/paganod.db")
                                        .Options;

                var dbContext = new AppDbContext(dbOptions);

                // if debug, this will be initalized when user creates a new database
                dbContext.InitalizeAsync().Wait();


                return dbContext;
            });

            return services;
        }
    }
}
