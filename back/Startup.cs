using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Linq;

namespace Paganod.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(configure =>
            {
                //configure.JsonSerializerOptions.Converters.Add(new DictionaryJsonConvertor());
            });

            //            services.AddScoped<ITenancyResolver>((serviceProvider) =>
            //            {
            //#if DEBUG
            //                return new TestTenantResolver();
            //#else
            //            var httpContext = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            //            return new TenantResolverServiceJwt(httpContext);
            //#endif

            //            });

            //services.AddCommonInterfaces(Configuration);

            services.AddConfigServices(Configuration);
            //services.AddDataServices(Configuration);
            //services.AddFileStorageServices(Configuration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Paganod.Api", Version = "v1" });
                c.ResolveConflictingActions(x => x.First());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Paganod.Api v1"));
            }

            app.UseCors(builder => builder.WithOrigins("http://localhost:5014", "https://localhost:7218")
                                          .AllowAnyMethod()
                                          .AllowAnyHeader());

            app.UseExceptionHandler(async context =>
            {



                //context.Response.ContentType = Text.Plain;
                //await context.Response.WriteAsync("An exception was thrown.");

            });

            //app.Use(next => context => {
            //    context.Request.EnableBuffering();
            //    return next(context);
            //});

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.AddDynamicEndpointRouting();
        }
    }
}
