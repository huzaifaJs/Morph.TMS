using MediatR;
using Morpho.Domain.Common;

namespace Morpho.Application.Queries
{
    /// <summary>
    /// Base class for queries that return data.
    /// </summary>
    /// <typeparam name="TResponse">The type of data returned</typeparam>
    public abstract class Query<TResponse> : IRequest<Result<TResponse>>
    {
    }
}