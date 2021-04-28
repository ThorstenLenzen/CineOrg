using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Toto.CineOrg.Persistence.Database;
using Toto.Utilities.Hosting;

namespace Toto.CineOrg.GraphQLApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .AddConfiguration(Configuration)
                .AddDbContext<CineOrgContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CineOrgDb")))
                .MigrateDb<CineOrgContext>();
        
        public static IConfigurationRoot Configuration
        {
            get
            {
                var environment = Environment
                    .GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                
                var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                    .Build();
                
                return configuration;
            }
        }
    }
}