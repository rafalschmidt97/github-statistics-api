using System.Collections.Generic;
using System.Linq;
using GithubStatistics.Application.Repositories.Infrastructure.Github;
using GithubStatistics.Application.Repositories.Infrastructure.Statistics;
using Xunit;

namespace GithubStatistics.Application.UnitTest.Repositories
{
    public class RepositoriesProcessorTest
    {
        [Fact]
        public void ShouldReturnFilledStatistics()
        {
            var repositories = new List<RepositoryDetails>
            {
                new RepositoryDetails("ABC", 5, 10, 20, 1000),
                new RepositoryDetails("a-c-e", 10, 20, 40, 2000),
            };

            var result = RepositoriesProcessor.PrepareStatistics("owner", repositories);

            Assert.Equal("owner", result.Owner);
            Assert.Equal(2, result.Letters['a']);
            Assert.Equal(2, result.Letters['c']);
            Assert.Equal(1, result.Letters['e']);
            Assert.False(result.Letters.ContainsKey('z'));
            Assert.False(result.Letters.ContainsKey('A')); // case insensitive
            Assert.False(result.Letters.ContainsKey('-')); // only letters
            Assert.Equal(15, result.AvgStargazers);
            Assert.Equal(30, result.AvgWatchers);
            Assert.Equal(7.5, result.AvgForks);
            Assert.Equal(1500, result.AvgSize);
        }

        [Fact]
        public void ShouldReturnEmptyStatistics()
        {
            var repositories = new List<RepositoryDetails>();

            var result = RepositoriesProcessor.PrepareStatistics("owner", repositories);

            Assert.Equal("owner", result.Owner);
            Assert.False(result.Letters.Any());
            Assert.Equal(0, result.AvgStargazers);
            Assert.Equal(0, result.AvgWatchers);
            Assert.Equal(0, result.AvgForks);
            Assert.Equal(0, result.AvgSize);
        }
    }
}
