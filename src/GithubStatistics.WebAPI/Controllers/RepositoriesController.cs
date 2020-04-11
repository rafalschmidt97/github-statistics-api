using System.Threading.Tasks;
using GithubStatistics.Application.Repositories.Queries.GetStatistics;
using Microsoft.AspNetCore.Mvc;

namespace GithubStatistics.WebAPI.Controllers
{
    public class RepositoriesController : ApiControllerBase
    {
        [HttpGet("{owner}")]
        public async Task<RepositoriesStatistics> Get(string owner)
        {
            return await Mediator.Send(new GetStatisticsQuery(owner));
        }
    }
}
