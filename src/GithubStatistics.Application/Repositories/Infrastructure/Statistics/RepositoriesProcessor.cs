using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GithubStatistics.Application.Repositories.Infrastructure.Github;
using GithubStatistics.Application.Repositories.Queries.GetStatistics;

namespace GithubStatistics.Application.Repositories.Infrastructure.Statistics
{
    public static class RepositoriesProcessor
    {
        public static RepositoriesStatistics PrepareStatistics(string owner, IList<RepositoryDetails> repositories)
        {
            if (!repositories.Any())
            {
                return new RepositoriesStatistics
                {
                    Owner = owner,
                    Letters = new Dictionary<char, int>(),
                    AvgStargazers = 0,
                    AvgWatchers = 0,
                    AvgForks = 0,
                    AvgSize = 0,
                };
            }

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

            return new RepositoriesStatistics
            {
                Owner = owner,
                Letters = letters,
                AvgStargazers = sumStargazers / repositories.Count,
                AvgWatchers = sumWatchers / repositories.Count,
                AvgForks = sumForks / repositories.Count,
                AvgSize = sumSize / repositories.Count,
            };
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
