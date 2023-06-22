using System.Diagnostics;
using Cyclst.CleanArchitecture.Application.Logging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Cyclst.CleanArchitecture.Application.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;
    private readonly ILogDetailsService _logDetails;

    public PerformanceBehaviour(ILogger<TRequest> logger, ILogDetailsService logDetails)
    {
        _timer = new Stopwatch();

        _logger = logger;
        _logDetails = logDetails;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > 500)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogWarning("CleanArchitecture Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}, {LogDetails}",
                requestName, elapsedMilliseconds, request, await _logDetails.GetDetails());
        }

        return response;
    }
}
