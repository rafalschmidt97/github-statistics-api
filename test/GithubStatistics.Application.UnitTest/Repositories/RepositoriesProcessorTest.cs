using System.Collections.Generic;
using FluentAssertions;
using GithubStatistics.Application.Repositories.Infrastructure.Github;
using GithubStatistics.Application.Repositories.Infrastructure.Statistics;
using GithubStatistics.Application.Repositories.Queries.GetStatistics;
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
                new RepositoryDetails { Name = "ABC", StargazersCount = 5, WatchersCount = 10, ForkCount = 20, DiskUsage = 1000 },
                new RepositoryDetails { Name = "a-c-e", StargazersCount = 10, WatchersCount = 20, ForkCount = 40, DiskUsage = 2000 },
            };

            var result = RepositoriesProcessor.PrepareStatistics("owner", repositories);

            result.Should().BeEquivalentTo(new RepositoriesStatistics
            {
                Owner = "owner",
                Letters = new Dictionary<char, int> { { 'a', 2 }, { 'b', 1 }, { 'c', 2 }, { 'e', 1 } },
                AvgStargazers = 7.5f,
                AvgWatchers = 15,
                AvgForks = 30,
                AvgSize = 1500,
            });
        }

        [Fact]
        public void ShouldReturnEmptyStatistics()
        {
            var repositories = new List<RepositoryDetails>();

            var result = RepositoriesProcessor.PrepareStatistics("owner", repositories);

            result.Should().BeEquivalentTo(new RepositoriesStatistics
            {
                Owner = "owner",
                Letters = new Dictionary<char, int>(),
                AvgStargazers = 0,
                AvgWatchers = 0,
                AvgForks = 0,
                AvgSize = 0,
            });
        }
    }
}
