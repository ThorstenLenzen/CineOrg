using System.Threading.Tasks;
using FluentAssertions;
using GraphQL.Client.Http;
using Toto.CineOrg.GraphQLApi.IntegrationTests.ResponseTypes;
using Toto.CineOrg.TestFramework;
using Xunit;

namespace Toto.CineOrg.GraphQLApi.IntegrationTests
{
    public class MovieQueryTest : IClassFixture<InMemorySqliteApplicationFactory<Startup>>
    {
        private const string Query = @"
        query AllMovies($filter: moviesQueryFilter) {
            movies(filter: $filter) {
                ...movieFields
            }
        },
        query MovieById($id: ID!) {
            movie (id: $id) {
                ...movieFields
            }
        },
        fragment movieFields on MovieQueryType {
            id,
            title,
            description,
            yearReleased,
            genre
        }
        ";
        
        private readonly GraphQLHttpClient _graphQlHttpClient;
        
        public MovieQueryTest(InMemorySqliteApplicationFactory<Startup> factory)
        {
            _graphQlHttpClient = factory.CreateGraphQlHttpClient();
        }
        
        [Fact]
        public async Task Can_Get_All_Movies()
        {
            var query = new GraphQLHttpRequest
            {
                Query = Query,
                OperationName = "AllMovies",
            };
            
            var response = await _graphQlHttpClient.SendQueryAsync<MovieResponseCollectionType>(query);

            response.Errors.Should().BeNull();
            response.Data.Movies.Should().NotBeNull();
            response.Data.Movies.Count.Should().BePositive();
        }

        [Fact]
        public async Task Can_Get_All_Movies_With_OrderBy_Filter()
        {
            var query = new GraphQLHttpRequest
            {
                Query = Query,
                OperationName = "AllMovies",
                Variables = new 
                { 
                    filter = new { orderBy = "title" }
                }
            };
            
            var response = await _graphQlHttpClient.SendQueryAsync<MovieResponseCollectionType>(query);

            response.Errors.Should().BeNull();
            
            var movieList = response.Data.Movies;
            movieList.Should().NotBeNull();
            movieList.Count.Should().BePositive();
            movieList.Should().BeInDescendingOrder(mov => mov.Title);
        }

        [Fact]
        public async Task Can_Get_All_Movies_With_Paging_Filter()
        {
            var query = new GraphQLHttpRequest
            {
                Query = Query,
                OperationName = "AllMovies",
                Variables = new 
                { 
                    filter = new { skip = 1, take = 1 }
                }
            };
            
            var response = await _graphQlHttpClient.SendQueryAsync<MovieResponseCollectionType>(query);

            response.Errors.Should().BeNull();
            
            var movieList = response.Data.Movies;
            movieList.Should().NotBeNull();
            movieList.Count.Should().BePositive();
        }

        [Fact]
        public async Task Can_Get_All_Movies_With_Paging_And_OrderBy_Filter()
        {
            var query = new GraphQLHttpRequest
            {
                Query = Query,
                OperationName = "AllMovies",
                Variables = new 
                { 
                    filter = new { orderBy = "title", skip = 0, take = 1 }
                }
            };
            
            var response = await _graphQlHttpClient.SendQueryAsync<MovieResponseCollectionType>(query);

            response.Errors.Should().BeNull();
            
            var movieList = response.Data.Movies;
            movieList.Should().NotBeNull();
            movieList.Count.Should().BePositive();
        }

        [Fact]
        public async Task Can_Get_Single_Movie_By_Id()
        {
            var query = new GraphQLHttpRequest
            {
                Query = Query,
                OperationName = "MovieById",
                Variables = new { id = "20db69d0-7760-4c3f-a484-032423e61018" }
            };
            
            var response = await _graphQlHttpClient.SendQueryAsync<MovieResponseType>(query);

            response.Errors.Should().BeNull();
            response.Data.Movie.Should().NotBeNull();
        }
    }
}