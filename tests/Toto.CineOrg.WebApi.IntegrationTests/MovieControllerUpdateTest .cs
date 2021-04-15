using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Toto.CineOrg.TestFramework;
using Toto.CineOrg.WebApi.Hosting;
using Xunit;

namespace Toto.CineOrg.WebApi.IntegrationTests
{
    public class MovieControllerUpdateTest : IClassFixture<InMemorySqliteApplicationFactory<Startup>>
    {
        private const string RequestUri = "/movie";
        private const string MovieGuid = "20DB69D0-7760-4C3F-A484-032423E61018";

        private readonly HttpClient _client;

        public MovieControllerUpdateTest(InMemorySqliteApplicationFactory<Startup> factory)
        
        {
            _client = factory.CreateClient();
        }
        
        [Fact]
        public async Task Can_Update_Movie()
        {
            var jsonContent = DataStructures
                .ValidMovieForUpdate
                .ToJsonContent();
            
            var response = await _client.PutAsync($"{RequestUri}/{MovieGuid}", jsonContent);

            response.EnsureStatusCode(HttpStatusCode.NoContent);
        }
    }
}