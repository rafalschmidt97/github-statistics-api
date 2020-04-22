using GithubStatistics.Application.Core.Bus;

namespace GithubStatistics.Application.Repositories.Queries.GetStatistics
{
    public class GetStatisticsQuery : IQuery<RepositoriesStatistics>
    {
        public GetStatisticsQuery(string owner)
        {
            Owner = owner;
        }

        public string Owner { get; }
    }
}
