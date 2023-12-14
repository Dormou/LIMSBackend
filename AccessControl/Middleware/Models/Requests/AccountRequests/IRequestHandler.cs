using AccessControl.Requests;
using AccessControl.Responses;

namespace AccessControl.Middleware.Models.Requests.AccountRequests;

public interface IRequestHandler<TRequest, TResponse>
{
    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
