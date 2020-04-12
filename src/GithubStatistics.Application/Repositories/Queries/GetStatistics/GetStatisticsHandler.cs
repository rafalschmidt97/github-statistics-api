using System.Collections.Generic;
using System.Text.RegularExpressions;
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

        public override async Task<RepositoriesStatistics> Handle(GetStatisticsQuery request)
        {
            var repositories = await _githubFetcher.GetRepositoriesDetails(request.Owner);

            var letters = new Dictionary<char, int>();
            float sumStargazers = 0,
                  sumWatchers = 0,
                  sumForks = 0,
                  sumSize = 0;

            foreach (var repository in repositories)
            {
                FillLetters(repository.Name, letters);
                sumStargazers += repository.StargazersCount;
                sumWatchers += repository.WatchersCount;
                sumForks += repository.ForkCount;
                sumSize += repository.DiskUsage;
            }

            return new RepositoriesStatistics(
                request.Owner,
                letters,
                sumStargazers / repositories.Count,
                sumWatchers / repositories.Count,
                sumForks / repositories.Count,
                sumSize / repositories.Count);
        }

        private static void FillLetters(string repositoryName, IDictionary<char, int> letters)
        {
            var simplified = Regex.Replace(repositoryName.ToLowerInvariant(), "[^a-z]", "");
            foreach (var letter in simplified)
            {
                if (letters.ContainsKey(letter))
                {
                    letters[letter] += 1;
                }
                else
                {
                    letters[letter] = 1;
                }
            }
        }
    }
}
