using System.Collections.Generic;

namespace GithubStatistics.Application.Repositories.Queries.GetStatistics
{
    public class RepositoriesStatistics
    {
        public string Owner { get; set; }
        public Dictionary<char, int> Letters { get; set; }
        public float AvgStargazers { get; set; }
        public float AvgWatchers { get; set; }
        public float AvgForks { get; set; }
        public float AvgSize { get; set; }
    }
}
