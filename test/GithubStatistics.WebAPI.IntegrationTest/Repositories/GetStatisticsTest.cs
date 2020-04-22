using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using GithubStatistics.Application.Repositories.Queries.GetStatistics;
using GithubStatistics.WebAPI.Extensions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace GithubStatistics.WebAPI.IntegrationTest.Repositories
{
    public class GetStatisticsTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public GetStatisticsTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ShouldReturnData()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/repositories/rafalschmidt97");
            var bodyString = await response.Content.ReadAsStringAsync();
            var body = JsonConvert.DeserializeObject<RepositoriesStatistics>(bodyString);

            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);
            body.Owner.Should().BeEquivalentTo("rafalschmidt97");
            body.AvgStargazers.Should().BeLessThan(10);
        }

        [Fact]
        public async Task ShouldThrowNotFound()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/repositories/rafalschmidt");
            var bodyString = await response.Content.ReadAsStringAsync();
            var body = JsonConvert.DeserializeObject<ExceptionResponse>(bodyString);

            HttpStatusCode.NotFound.Should().BeEquivalentTo(response.StatusCode);
            body.Message.Should().BeEquivalentTo("User 'rafalschmidt' not found");
        }
    }
}
