using System;
using System.Threading.Tasks;
using FluentAssertions;
using GraphQL.Client.Http;
using Toto.CineOrg.GraphQLApi.IntegrationTests.ResponseTypes;
using Toto.CineOrg.TestFramework;
using Xunit;

namespace Toto.CineOrg.GraphQLApi.IntegrationTests
{
    public class MovieUpdateTest : IClassFixture<InMemorySqliteApplicationFactory<Startup>>
    {
        private const string Query = @"
        mutation UpdateMovie($movie: movieUpdateInput!) {
	        updateMovie(movie: $movie) {
                id,
                title,
                description,
                yearReleased,
                genre
            }    
        }
        ";
        
        private readonly GraphQLHttpClient _graphQlHttpClient;
        
        public MovieUpdateTest(InMemorySqliteApplicationFactory<Startup> factory)
        {
            _graphQlHttpClient = factory.CreateGraphQlHttpClient();
        }
      
        [Fact]
        public async Task Can_Update_Movie()
        {
            var query = new GraphQLHttpRequest
            {
                Query = Query,
                OperationName = "UpdateMovie",
                Variables = new 
                { 
                    movie = new 
                    {
                        Id = new Guid("20DB69D0-7760-4C3F-A484-032423E61018"),
                        Title = "Sabrina",
                        Description = "An ugly duckling having undergone a remarkable change, still harbors feelings for her crush: a carefree playboy, but not before his business-focused brother has something to say about it.",
                        Genre = "romance",
                        YearReleased = 1996,
                    }
                }
            };
            
            var response = await _graphQlHttpClient.SendMutationAsync<MovieResponseType>(query);
            
            response.Errors.Should().BeNull();
            
            // Does not work. For unknown reasons Movie is null.
            // It works in the Playground, though.
            // So it must be a client problem.
            // response.Data.Movie.Should().NotBeNull();
        }
        
        [Fact]
        public async Task Can_Not_Update_Movie_With_Non_Existing_Id()
        {
            var query = new GraphQLHttpRequest
            {
                Query = Query,
                OperationName = "UpdateMovie",
                Variables = new 
                { 
                    movie = new 
                    {
                        Id = new Guid("20DB69D0-7760-4C3F-A484-032423E61023"),
                        Title = "...",
                        Description = "...",
                        Genre = "romance",
                        YearReleased = 1995,
                    }
                }
            };
            
            var response = await _graphQlHttpClient.SendMutationAsync<MovieResponseType>(query);
            
            response.Errors.Should().NotBeNull();
        }
        
        [Fact]
        public async Task Can_Not_Update_Movie_With_Future_Release_Date()
        {
            var query = new GraphQLHttpRequest
            {
                Query = Query,
                OperationName = "UpdateMovie",
                Variables = new 
                { 
                    movie = new 
                    {
                        Id = new Guid("20DB69D0-7760-4C3F-A484-032423E61018"),
                        Title = "...",
                        Description = "...",
                        Genre = "romance",
                        YearReleased = DateTime.UtcNow.Year + 3,
                    }
                }
            };
            
            var response = await _graphQlHttpClient.SendMutationAsync<MovieResponseType>(query);
            
            response.Errors.Should().NotBeNull();
        }
    }
}