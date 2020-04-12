namespace GithubStatistics.Application.Repositories.Infrastructure.Github
{
    public class RepositoryDetails
    {
        public RepositoryDetails(string name, int forkCount, int stargazersCount, int watchersCount, int diskUsage)
        {
            Name = name;
            ForkCount = forkCount;
            StargazersCount = stargazersCount;
            WatchersCount = watchersCount;
            DiskUsage = diskUsage;
        }

        public string Name { get; }
        public int ForkCount { get; }
        public int StargazersCount { get; }
        public int WatchersCount { get; }
        public int DiskUsage { get; }
    }
}
