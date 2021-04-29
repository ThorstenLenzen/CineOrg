using System.Threading.Tasks;
using FluentAssertions;
using Toto.CineOrg.TestFramework;
using Xunit;
using GraphQL.Client.Http;
using Toto.CineOrg.GraphQLApi.IntegrationTests.ResponseTypes;

namespace Toto.CineOrg.GraphQLApi.IntegrationTests
{
    public class GenreQueryTest : IClassFixture<InMemorySqliteApplicationFactory<Startup>>
    {
        private readonly GraphQLHttpClient _graphQlHttpClient;
        
        public GenreQueryTest(InMemorySqliteApplicationFactory<Startup> factory)
        {
            _graphQlHttpClient = factory.CreateGraphQlHttpClient();
        }
        
        [Fact]
        public async Task Can_Get_All_Genres()
        {
            var query = new GraphQLHttpRequest
            {
                Query = @"{ genres { name }}"
            };
            
            var response = await _graphQlHttpClient.SendQueryAsync<GenresResponseCollectionType>(query);

            response.Errors.Should().BeNull();
            response.Data.Genres.Count.Should().BePositive();
        }
    }
}