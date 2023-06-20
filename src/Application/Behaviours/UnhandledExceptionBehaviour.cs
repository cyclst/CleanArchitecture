using CleanArchitecture.Application.Logging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Behaviours;

public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;
    private readonly ILogDetailsService _logDetails;

    public UnhandledExceptionBehaviour(ILogger<TRequest> logger, ILogDetailsService logDetails)
    {
        _logger = logger;
        _logDetails = logDetails;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogError(ex, "CleanArchitecture Request: Unhandled Exception for Request {Name} {@Request} {LogDetails}", 
                requestName, request, await _logDetails.GetDetails());

            throw;
        }
    }
}
