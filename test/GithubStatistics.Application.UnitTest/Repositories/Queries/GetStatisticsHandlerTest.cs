using System.Collections.Generic;
using System.Threading.Tasks;
using GithubStatistics.Application.Repositories.Infrastructure.Github;
using GithubStatistics.Application.Repositories.Queries.GetStatistics;
using GithubStatistics.Common.Exceptions;
using Moq;
using Xunit;

namespace GithubStatistics.Application.UnitTest.Repositories.Queries
{
    public class GetStatisticsHandlerTest
    {
        private readonly Mock<IGithubFetcher> _githubFetcher;

        public GetStatisticsHandlerTest()
        {
            _githubFetcher = new Mock<IGithubFetcher>();
        }

        [Fact]
        public async Task ShouldReturnStatistics()
        {
            var request = new GetStatisticsQuery("rafalschmidt97");
            _githubFetcher
                .Setup(x => x.GetRepositoriesDetails(It.IsAny<string>(), true))
                .ReturnsAsync(GetSampleRepositoryDetails);
            var handler = new GetStatisticsHandler(_githubFetcher.Object);

            var result = await handler.Handle(request);

            Assert.Equal("rafalschmidt97", result.Owner);
            Assert.Equal(3, result.Letters['v']);
            Assert.Equal(1, result.Letters['a']);
            Assert.False(result.Letters.ContainsKey('z'));
            Assert.False(result.Letters.ContainsKey('A')); // case insensitive
            Assert.False(result.Letters.ContainsKey('-')); // only letters
            Assert.Equal(10, result.AvgStargazers);
            Assert.Equal(10, result.AvgWatchers);
            Assert.Equal(10, result.AvgForks);
            Assert.Equal(1000, result.AvgSize);
        }

        [Fact]
        public async Task ShouldThrowNotFound()
        {
            var request = new GetStatisticsQuery("fakerafalschmidt");
            _githubFetcher
                .Setup(x => x.GetRepositoriesDetails(It.IsAny<string>(), true))
                .Throws<NotFoundException>();
            var handler = new GetStatisticsHandler(_githubFetcher.Object);

            await Assert.ThrowsAsync<NotFoundException>(async () => { await handler.Handle(request); });
        }

        private static IList<RepositoryDetails> GetSampleRepositoryDetails()
        {
            return new List<RepositoryDetails>
            {
                new RepositoryDetails("skelvy-api", 5, 5, 5, 1000),
                new RepositoryDetails("skelvy-client", 10, 10, 10, 1000),
                new RepositoryDetails("skelvy-website", 15, 15, 15, 1000),
            };
        }
    }
}
