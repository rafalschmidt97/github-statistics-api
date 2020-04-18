namespace GithubStatistics.Application.Repositories.Infrastructure.Github
{
    public class RepositoryDetails
    {
        public string Name { get; set; }
        public int ForkCount { get; set; }
        public int StargazersCount { get; set; }
        public int WatchersCount { get; set; }
        public int DiskUsage { get; set; }
    }
}
