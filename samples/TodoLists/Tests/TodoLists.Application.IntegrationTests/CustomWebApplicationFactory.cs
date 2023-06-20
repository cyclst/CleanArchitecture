using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using TodoLists.Infrastructure.Identity;
using TodoLists.Infrastructure.Persistence;

namespace TodoLists.Application.IntegrationTests;

using static Testing;

internal class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
            var integrationConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            configurationBuilder.AddConfiguration(integrationConfig);
        });

        builder.ConfigureServices((builder, services) =>
        {
            services
                .Remove<ICurrentUserService>()
                .AddTransient(provider => Mock.Of<ICurrentUserService>(s =>
                    s.UserId == GetCurrentUserId()));

            services
                .Remove<DbContextOptions<TodoListsDbContext>>()
                .AddDbContext<TodoListsDbContext>((sp, options) =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                        builder => builder.MigrationsAssembly(typeof(TodoListsDbContext).Assembly.FullName)));
        });
    }
}
