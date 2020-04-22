using MediatR;

namespace GithubStatistics.Application.Core.Bus
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
