using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using GithubStatistics.Application.Repositories.Queries.GetStatistics;
using GithubStatistics.WebAPI.Extensions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace GithubStatistics.WebAPI.IntegrationTest.Repositories
{
    public class RepositoriesTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public RepositoriesTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetRepositoriesShouldReturnData()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/repositories/rafalschmidt97");
            var bodyString = await response.Content.ReadAsStringAsync();
            var body = JsonSerializer.Deserialize<RepositoriesStatistics>(bodyString);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("rafalschmidt97", body.Owner);
            Assert.InRange(body.AvgStargazers, 0, 10);
        }

        [Fact]
        public async Task GetRepositoriesThrowNotFound()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/repositories/fakerafalschmidt");
            var bodyString = await response.Content.ReadAsStringAsync();
            var body = JsonSerializer.Deserialize<ExceptionResponse>(bodyString);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal("User 'fakerafalschmidt' not found", body.Message);
        }
    }
}
