using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Toto.CineOrg.TestFramework;
using Toto.CineOrg.WebApi.Hosting;
using Xunit;

namespace Toto.CineOrg.WebApi.IntegrationTests
{
    public class MovieControllerCreateTest : IClassFixture<InMemorySqliteApplicationFactory<Startup>>
    {
        private const string ExpectedContentType = "application/json; charset=utf-8";
        private const string RequestUri = "/movie";

        private readonly HttpClient _client;

        public MovieControllerCreateTest(InMemorySqliteApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }
        
        [Fact]
        public async Task Can_Create_Movie()
        {
            var jsonContent = DataStructures
                .ValidMovieForCreate
                .ToJsonContent();

            var response = await _client.PostAsync(RequestUri, jsonContent);

            response.EnsureStatusCode(HttpStatusCode.Created);
            response.GetContentType().Should().Be(ExpectedContentType);
        }
    }
}