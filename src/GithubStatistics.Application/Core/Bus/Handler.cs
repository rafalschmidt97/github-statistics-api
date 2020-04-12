using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace GithubStatistics.Application.Core.Bus
{
    public abstract class Handler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IQuery<TResponse>
    {
        public abstract Task<TResponse> Handle(TRequest request);

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
            => Handle(request);
    }
}
