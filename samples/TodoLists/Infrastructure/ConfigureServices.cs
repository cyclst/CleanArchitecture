using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TodoLists.Infrastructure.Persistence;
using TodoLists.Infrastructure.Identity;
using TodoLists.Application.Persistence;
using TodoItems.Infrastructure.Persistence;
using TodoLists.Infrastructure.Services;
using TodoLists.Infrastructure.Persistence.Interceptors;
using Cyclst.CleanArchitecture.EntityFramework;
using MediatR;
using System.Reflection;
using TodoLists.Infrastructure.Identity.Behaviours;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<TodoListsDbContext>(options =>
                options.UseInMemoryDatabase("TodoListsDb"));
        }
        else
        {
            services.AddDbContext<TodoListsDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(TodoListsDbContext).Assembly.FullName)));
        }

        services.AddScoped<IDbContext>(provider => provider.GetRequiredService<TodoListsDbContext>());

        services.AddScoped<TodoListsDbContextInitialiser>();

        services.AddScoped<ITodoItemRepository, TodoItemRepository>();
        services.AddScoped<ITodoListRepository, TodoListRepository>();

        services.AddTransient<IDateTime, DateTimeService>();

        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddTransient<IIdentityService, IdentityService>();

        services
            .AddDefaultIdentity<TodoListsUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<TodoListsDbContext>();

        services.AddIdentityServer()
            .AddApiAuthorization<TodoListsUser, TodoListsDbContext>();

        services.AddAuthentication()
            .AddIdentityServerJwt();

        services.AddAuthorization(options =>
            options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));

        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));

        });

        return services;
    }
}
