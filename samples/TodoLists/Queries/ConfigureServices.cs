using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TodoLists.Queries.TodoLists.ExportTodos.Files;

namespace TodoLists.Queries;

public static class ConfigureServices
{
    public static IServiceCollection AddQueryServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

        });

        services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

        return services;
    }
}
