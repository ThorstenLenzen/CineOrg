using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Extensions.Logging;
using Serilog.Formatting.Compact;
using Toto.CineOrg.Persistence.Database;
using Toto.CineOrg.WebApi.Hosting;

namespace Toto.CineOrg.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = ConfigureLogger();

            try
            {
                Log.Information("Starting web host");
                
                CreateHostBuilder(args)
                    .Build()
                    .Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host
           .CreateDefaultBuilder(args)
           .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
           .AddConfiguration(Configuration)
           .UseSerilog()
           .AddDbContext<CineOrgContext>(options => options
               .UseSqlServer(Configuration.GetConnectionString("CineOrgDb"))
               .UseLoggerFactory(new SerilogLoggerFactory(Log.Logger))  //tie-up DbContext with LoggerFactory object
               .EnableSensitiveDataLogging())
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

        public static Logger ConfigureLogger()
        {
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {NewLine}{Exception}")
                .WriteTo.RollingFile(new RenderedCompactJsonFormatter(),$"CineOrgLog-{DateTime.UtcNow:dd-MM-yyyy}.json")
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();

            return logger;
        }
    }
}