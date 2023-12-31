﻿using Cyclst.CleanArchitecture.EntityFramework;
using Cyclst.CleanArchitecture.Application.Persistence;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddCommonEntityFrameworkServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, EFUnitOfWork>();

        return services;
    }
}
