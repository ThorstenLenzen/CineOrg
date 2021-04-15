using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Toto.CineOrg.ServiceModel;
using Toto.CineOrg.TestFramework;
using Toto.CineOrg.WebApi.Hosting;
using Xunit;

namespace Toto.CineOrg.WebApi.IntegrationTests
{
    public class MovieControllerQueryTest : IClassFixture<InMemorySqliteApplicationFactory<Startup>>
    {
        private const string ExpectedContentType = "application/json; charset=utf-8";
        private const string RequestUri = "/movie";
        private const string MovieGuid = "20DB69D0-7760-4C3F-A484-032423E61018";

        private readonly InMemorySqliteApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public MovieControllerQueryTest(InMemorySqliteApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }
        
        [Fact]
        public async Task Can_Get_All_Movies()
        {
            var response = await _client.GetAsync(RequestUri);
            // var content = await response.Content.ReadAsStringAsync();
            
            response.EnsureStatusCode(HttpStatusCode.OK);
            response.GetContentType().Should().Be(ExpectedContentType);
        }
        
        [Fact]
        public async Task Can_Get_All_Movies_With_OrderBy_Filter()
        {
            var response = await _client.GetAsync($"{RequestUri}?orderBy=created");
            
            response.EnsureStatusCode(HttpStatusCode.OK);
            response.GetContentType().Should().Be(ExpectedContentType);
            
            var movies =  await response.Content.ToListAsync<Movie>();
            movies[0].Title.Should().Be("Sabrina");
        }
        
        [Fact]
        public async Task Can_Get_All_Movies_With_Paging_Filter()
        {
            var response = await _client.GetAsync($"{RequestUri}?skip=1&take=1");
            
            response.EnsureStatusCode(HttpStatusCode.OK);
            response.GetContentType().Should().Be(ExpectedContentType);
            
            var movies =  await response.Content.ToListAsync<Movie>();
            movies.Count.Should().Be(1);
            movies[0].Title.Should().Be("Sabrina");
        }
        
        [Fact]
        public async Task Can_Get_All_Movies_With_Paging_And_OrderBy_Filter()
        {
            var response = await _client.GetAsync($"{RequestUri}?skip=1&take=1&orderBy=created");
            
            response.EnsureStatusCode(HttpStatusCode.OK);
            response.GetContentType().Should().Be(ExpectedContentType);
            
            var movies =  await response.Content.ToListAsync<Movie>();
            movies.Count.Should().Be(1);
            movies[0].Title.Should().Be("Patriot Games");
        }
        
        [Fact]
        public async Task Can_Get_Movie_By_Id()
        {
            var response = await _client.GetAsync($"{RequestUri}/{MovieGuid}");

            response.EnsureStatusCode(HttpStatusCode.OK);
            response.GetContentType().Should().Be(ExpectedContentType);
        }
    }
}