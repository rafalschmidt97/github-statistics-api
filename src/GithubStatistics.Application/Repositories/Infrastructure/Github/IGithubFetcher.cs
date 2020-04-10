using System.Collections.Generic;
using System.Threading.Tasks;

namespace GithubStatistics.Application.Repositories.Infrastructure.Github
{
    public interface IGithubFetcher
    {
        Task<IList<RepositoryDetails>> GetRepositoriesDetails(string owner, bool all = true);
    }
}
