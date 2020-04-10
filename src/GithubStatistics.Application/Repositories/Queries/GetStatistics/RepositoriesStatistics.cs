using System.Collections.Generic;

namespace GithubStatistics.Application.Repositories.Queries.GetStatistics
{
    public class RepositoriesStatistics
    {
        public RepositoriesStatistics(
            string owner,
            Dictionary<char, int> letters,
            float avgStargazers,
            float avgWatchers,
            float avgForks,
            float avgSize)
        {
            Owner = owner;
            Letters = letters;
            AvgStargazers = avgStargazers;
            AvgWatchers = avgWatchers;
            AvgForks = avgForks;
            AvgSize = avgSize;
        }

        public string Owner { get; }
        public Dictionary<char, int> Letters { get; }
        public float AvgStargazers { get; }
        public float AvgWatchers { get; }
        public float AvgForks { get; }
        public float AvgSize { get; }
    }
}
