using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Toto.CineOrg.TestFramework;
using Toto.CineOrg.WebApi.Hosting;
using Xunit;

namespace Toto.CineOrg.WebApi.IntegrationTests
{
    public class GenreControllerTest : IClassFixture<InMemorySqliteApplicationFactory<Startup>>
    {
        private const string ExpectedContentType = "application/json; charset=utf-8";
        private const string RequestUri = "/genre";

        private readonly InMemorySqliteApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public GenreControllerTest(InMemorySqliteApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }
        
        [Fact]
        public async Task Can_Get_All_Genres()
        {
            var response = await _client.GetAsync(RequestUri);
            // var content = await response.Content.ReadAsStringAsync();
            
            response.EnsureStatusCode(HttpStatusCode.OK);
            response.GetContentType().Should().Be(ExpectedContentType);
        }
    }
}