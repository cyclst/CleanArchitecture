using CleanArchitecture.AspNetCoreIdentity;
using CleanArchitecture.AspNetCoreIdentity.Behaviours;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddCommonAspNetCoreIdentityServices(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddTransient<IIdentityService, IdentityService>();

        return services;
    }
}
