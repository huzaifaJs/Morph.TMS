using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Morpho.Domain.Common;

namespace Morpho.Application.Behaviors
{
    /// <summary>
    /// Pipeline behavior for validating requests using FluentValidation.
    /// </summary>
    /// <typeparam name="TRequest">The request type</typeparam>
    /// <typeparam name="TResponse">The response type</typeparam>
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class, IRequest<TResponse>
        where TResponse : Result
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(
                    _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failures = validationResults
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Any())
                {
                    var errorMessage = string.Join("; ", failures.Select(f => f.ErrorMessage));
                    
                    // Use reflection to create the appropriate Result failure
                    var resultType = typeof(TResponse);
                    if (resultType.IsGenericType && resultType.GetGenericTypeDefinition() == typeof(Result<>))
                    {
                        var valueType = resultType.GetGenericArguments()[0];
                        var failureMethod = typeof(Result).GetMethod(nameof(Result.Failure))
                            ?.MakeGenericMethod(valueType);
                        return (TResponse)failureMethod?.Invoke(null, new object[] { errorMessage });
                    }
                    else
                    {
                        var failureMethod = typeof(Result).GetMethod(nameof(Result.Failure), new[] { typeof(string) });
                        return (TResponse)failureMethod?.Invoke(null, new object[] { errorMessage });
                    }
                }
            }

            return await next();
        }
    }
}