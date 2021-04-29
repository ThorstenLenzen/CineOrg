using System.Threading.Tasks;
using FluentAssertions;
using GraphQL.Client.Http;
using Toto.CineOrg.GraphQLApi.IntegrationTests.ResponseTypes;
using Toto.CineOrg.TestFramework;
using Xunit;

namespace Toto.CineOrg.GraphQLApi.IntegrationTests
{
    public class SeatCategoriesGraphQlApiTest : IClassFixture<InMemorySqliteApplicationFactory<Startup>>
    {
        private readonly GraphQLHttpClient _graphQlHttpClient;
        
        public SeatCategoriesGraphQlApiTest(InMemorySqliteApplicationFactory<Startup> factory)
        {
            _graphQlHttpClient = factory.CreateGraphQlHttpClient();
        }
        
        [Fact]
        public async Task Can_Get_All_SeatCategories()
        {
            var query = new GraphQLHttpRequest
            {
                Query = @"{ seatCategories { name }}"
            };
            
            var response = await _graphQlHttpClient.SendQueryAsync<SeatCategoriesResponseCollectionType>(query);

            response.Errors.Should().BeNull();
            response.Data.SeatCategories.Count.Should().BePositive();
        }
    }
}