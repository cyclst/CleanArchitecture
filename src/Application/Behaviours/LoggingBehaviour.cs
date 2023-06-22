using Cyclst.CleanArchitecture.Application.Logging;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Cyclst.CleanArchitecture.Application.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;
    private readonly ILogDetailsService _logDetails;

    public LoggingBehaviour(ILogger<TRequest> logger, ILogDetailsService logDetails
        )
    {
        _logger = logger;
        _logDetails = logDetails;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        _logger.LogDebug("CleanArchitecture Request: {Name} {@Request} {LogDetails}",
            requestName, request, await _logDetails.GetDetails());
    }
}
