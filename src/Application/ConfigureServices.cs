using System.Reflection;
using CleanArchitecture.Application.Behaviours;
using CleanArchitecture.Application.Logging;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddCommonApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<ILogDetailsService, LogDetailsService>();
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            cfg.AddBehavior(typeof(IRequestPreProcessor<>), typeof(LoggingBehaviour<>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

        });

        return services;
    }
}
