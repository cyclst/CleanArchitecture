﻿using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace TodoLists.Queries;

public static class ConfigureServices
{
    public static IServiceCollection AddQueryServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

        });

        return services;
    }
}
