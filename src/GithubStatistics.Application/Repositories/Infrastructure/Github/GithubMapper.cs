using System.Collections.Generic;
using System.Linq;

namespace GithubStatistics.Application.Repositories.Infrastructure.Github
{
    public static class GithubMapper
    {
        public static IList<RepositoryDetails> ToDetails(IList<GithubRepositoryResponse> responses)
        {
            return responses
                .Select(repository => new RepositoryDetails
                {
                    Name = repository.Name,
                    ForkCount = repository.ForkCount,
                    StargazersCount = repository.Stargazers.TotalCount,
                    WatchersCount = repository.Watchers.TotalCount,
                    DiskUsage = repository.DiskUsage,
                }).ToList();
        }
    }
}
