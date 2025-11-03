using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Comments.Core.Application;
using MyApp.Comments.Core.Domain;
using MyApp.Comments.Infrastructure.Persistence;

namespace MyApp.Comments.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IRepository<Comment>, Repository<Comment>>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
