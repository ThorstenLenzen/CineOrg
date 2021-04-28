using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Toto.Utilities.Hosting
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder AddConfiguration(this IHostBuilder hostBuilder, IConfiguration configuration)
        {
            hostBuilder.ConfigureServices(services => { services.AddSingleton(configuration); });
            return hostBuilder;
        }
        
        public static IHostBuilder AddDbContext<TDbContext>(this IHostBuilder hostBuilder, Action<DbContextOptionsBuilder>? options = null)
            where TDbContext : DbContext
        {
            hostBuilder.ConfigureServices(services => services.AddDbContext<TDbContext>(options));
            return hostBuilder;
        }

        public static IHostBuilder MigrateDb<TDbContext>(this IHostBuilder hostBuilder) where TDbContext : DbContext
        {
            hostBuilder.ConfigureServices(services =>
            {
                // Build an intermediate service provider
                var serviceProvider = services.BuildServiceProvider();
              
                // Prepare database
                var context = serviceProvider.GetRequiredService<TDbContext>();
                context.Database.Migrate();
            });

            return hostBuilder;
        }
    }
}