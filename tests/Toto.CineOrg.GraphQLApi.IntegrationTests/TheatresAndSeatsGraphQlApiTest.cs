using System.Threading.Tasks;
using FluentAssertions;
using GraphQL.Client.Http;
using Toto.CineOrg.GraphQLApi.IntegrationTests.ResponseTypes;
using Toto.CineOrg.TestFramework;
using Xunit;

namespace Toto.CineOrg.GraphQLApi.IntegrationTests
{
    public class TheatresAndSeatsGraphQlApiTest : IClassFixture<InMemorySqliteApplicationFactory<Startup>>
    {
        private const string Query = @"
        query AllTheatres {
            theatres {
                id,
                name,
                seats {
                    ...seatFields
                }
            }
        },
        query AllSeats {
            seats {
                ...seatFields
            }
        },
        fragment seatFields on SeatQueryType {
            id,
            rowletter,
            seatNumber,
            category
        }
        ";
        
        private readonly GraphQLHttpClient _graphQlHttpClient;
        
        public TheatresAndSeatsGraphQlApiTest(InMemorySqliteApplicationFactory<Startup> factory)
        {
            _graphQlHttpClient = factory.CreateGraphQlHttpClient();
        }
        
        [Fact]
        public async Task Can_Get_All_Theatres()
        {
            var query = new GraphQLHttpRequest
            {
                Query = Query,
                OperationName = "AllTheatres",
            };
            
            var response = await _graphQlHttpClient.SendQueryAsync<TheatreResponseCollectionType>(query);

            response.Errors.Should().BeNull();
            response.Data.Theatres.Count.Should().BePositive();
        }
        
        [Fact]
        public async Task Can_Get_All_Seats()
        {
            var query = new GraphQLHttpRequest
            {
                Query = Query,
                OperationName = "AllSeats",
            };
            
            var response = await _graphQlHttpClient.SendQueryAsync<SeatResponseCollectionType>(query);

            response.Errors.Should().BeNull();
            response.Data.Seats.Count.Should().BePositive();
        }
    }
}