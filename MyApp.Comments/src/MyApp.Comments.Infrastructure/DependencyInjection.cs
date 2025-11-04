using Microsoft.Extensions.DependencyInjection;
using MyApp.Comments.Contracts;
using MyApp.Comments.Infrastructure.Persistence;

namespace MyApp.Comments.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
