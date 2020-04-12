using System.Collections.Generic;
using System.Linq;
using GithubStatistics.Application.Repositories.Infrastructure.Github;

namespace GithubStatistics.Infrastructure.Repositories
{
    public static class GithubMapper
    {
        public static IList<RepositoryDetails> ToDetails(IList<GithubRepositoryResponse> responses)
        {
            return responses
                .Select(repository => new RepositoryDetails(
                    repository.Name,
                    repository.ForkCount,
                    repository.Stargazers.TotalCount,
                    repository.Watchers.TotalCount,
                    repository.DiskUsage))
                .ToList();
        }
    }
}
