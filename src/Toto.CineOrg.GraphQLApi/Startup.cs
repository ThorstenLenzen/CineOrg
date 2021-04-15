using GraphQL.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Toto.CineOrg.GraphQLApi.GraphQL;
using Toto.CineOrg.Hosting;
using Toto.CineOrg.Persistence.Database;

namespace Toto.CineOrg.GraphQLApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCqrsInfrastructure();
            services.AddCommandHandlers();
            services.AddQueryHandlers();
            
            services.AddScoped<QueryCineOrgContext>();
            
            services
                .AddScoped<CineOrgSchema>()
                .AddGraphQL((options, provider) =>
                {
                    options.EnableMetrics = true;
                    var logger = provider.GetRequiredService<ILogger<Startup>>();
                    options.UnhandledExceptionDelegate = ctx => 
                        logger.LogError("{Error} occurred", ctx.OriginalException.Message);
                })
                .AddSystemTextJson()
                .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = true)
                .AddGraphTypes(typeof(CineOrgSchema), ServiceLifetime.Scoped)
                .AddDataLoader();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseGraphQL<CineOrgSchema>();
            app.UseGraphQLPlayground();
        }
    }
}