using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Toto.CineOrg.Hosting;
using Toto.CineOrg.Persistence.Database;
using Toto.CineOrg.WebApi.Filters;

namespace Toto.CineOrg.WebApi.Hosting
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddCors(c =>  
            {  
                c.AddPolicy("AllowOrigin", options => options
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());  
            });
            
            services.AddScoped<QueryCineOrgContext>();
            
            services
                .AddMvc(options =>
                {
                    options.Filters.Add(new ValidateServiceModelFilterAttribute());
                })
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddFluentValidation();

            services.AddFluentValidators();
            services.AddCqrsInfrastructure();
            services.AddCommands();
            services.AddQueries();
            services.AddControllerQueries();
            services.AddControllerCommands();

            services.AddSwaggerGeneration();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            // Write streamlined request completion events, instead of the more verbose ones from the framework.
            // To use the default framework request logging instead, remove this line and set the "Microsoft"
            // level in appsettings.json to "Information".
            app.UseSerilogRequestLogging();
            
            app.UseSwaggerGeneration();
            app.UseExceptionHandlingMiddleware();
            
            // if (env.IsDevelopment())
            //     app.UseDeveloperExceptionPage();

            app.UseRouting();
            
            app.UseCors(options => options
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}