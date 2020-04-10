using System;
using System.Threading.Tasks;
using GithubStatistics.Application.Core.Bus;
using GithubStatistics.Application.Repositories.Infrastructure.Github;

namespace GithubStatistics.Application.Repositories.Queries.GetStatistics
{
    public class GetStatisticsHandler : Handler<GetStatisticsQuery, RepositoriesStatistics>
    {
        private readonly IGithubFetcher _githubFetcher;

        public GetStatisticsHandler(IGithubFetcher githubFetcher)
        {
            _githubFetcher = githubFetcher;
        }

        public override Task<RepositoriesStatistics> Handle(GetStatisticsQuery request)
        {
            throw new NotImplementedException();
        }
    }
}
