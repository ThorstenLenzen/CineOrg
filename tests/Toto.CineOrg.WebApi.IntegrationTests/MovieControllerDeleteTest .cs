using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Toto.CineOrg.TestFramework;
using Toto.CineOrg.WebApi.Hosting;
using Xunit;

namespace Toto.CineOrg.WebApi.IntegrationTests
{
    public class MovieControllerDeleteTest : IClassFixture<InMemorySqliteApplicationFactory<Startup>>
    {
        private const string RequestUri = "/movie";
        private const string MovieToDeleteGuid = "428429C0-9108-401C-B571-A09DC156AE50";

        private readonly HttpClient _client;

        public MovieControllerDeleteTest(InMemorySqliteApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Can_Delete_Movie()
        {
            var response = await _client.DeleteAsync($"{RequestUri}/{MovieToDeleteGuid}");

            response.EnsureStatusCode(HttpStatusCode.NoContent);
        }
        
        [Fact]
        public async Task Can_Not_Delete_Movie_With_Invalid_Id()
        {
            var response = await _client.DeleteAsync($"{RequestUri}/{Guid.NewGuid()}");
            
            response.EnsureStatusCode(HttpStatusCode.NotFound);
        }
        
        [Fact]
        public async Task Can_Not_Delete_Movie_With_Empty_Id()
        {
            var response = await _client.DeleteAsync($"{RequestUri}/{Guid.Empty}");

            response.EnsureStatusCode(HttpStatusCode.NotFound);
        }
    }
}