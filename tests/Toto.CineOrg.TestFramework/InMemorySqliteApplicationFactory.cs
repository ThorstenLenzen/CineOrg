using System;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Extensions.Logging;
using Toto.CineOrg.Persistence.Database;
using Toto.CineOrg.WebApi.Hosting;

namespace Toto.CineOrg.TestFramework
{
    public class InMemorySqliteApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        private readonly SqliteConnection _connection;
        private readonly Logger _logger;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IConfigurationRoot _configuration;
        
        public InMemorySqliteApplicationFactory()
        {
            const string connectionString = "Data Source=:memory:";
            const string outputTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {NewLine}{Exception}";
            
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            
            _logger = new LoggerConfiguration()
                .ReadFrom.Configuration(_configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: outputTemplate)
                .CreateLogger();
            
            _loggerFactory = new SerilogLoggerFactory(_logger);
            
            _connection = new SqliteConnection(connectionString);
            _connection.Open();
        }
        
        protected override IHostBuilder CreateHostBuilder()
        {
            Log.Logger = _logger;
            
            var builder = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<TStartup>(); })
                .AddConfiguration(_configuration)
                .UseSerilog()
                .AddDbContext<CineOrgContext>(options =>
                {
                    options.UseSqlite(_connection)
                       .UseLoggerFactory(_loggerFactory)  //tie-up DbContext with LoggerFactory object
                       .EnableSensitiveDataLogging();
                })
                .MigrateDb<CineOrgContext>();

            return builder;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _connection.Close();
        }

        public GraphQLHttpClient CreateGraphQlHttpClient(string relativeUrl = "graphql")
        {
            var httpClient = CreateClient();
            httpClient.BaseAddress = new Uri(httpClient.BaseAddress.AbsoluteUri + relativeUrl);
            
            return new GraphQLHttpClient(
                new GraphQLHttpClientOptions(),
                new NewtonsoftJsonSerializer(),
                httpClient);
        }
    }
}