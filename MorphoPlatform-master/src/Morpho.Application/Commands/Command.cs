using MediatR;
using Morpho.Domain.Common;

namespace Morpho.Application.Commands
{
    /// <summary>
    /// Base class for commands that don't return a value.
    /// </summary>
    public abstract class Command : IRequest<Result>
    {
    }

    /// <summary>
    /// Base class for commands that return a value.
    /// </summary>
    /// <typeparam name="TResponse">The type of response</typeparam>
    public abstract class Command<TResponse> : IRequest<Result<TResponse>>
    {
    }
}