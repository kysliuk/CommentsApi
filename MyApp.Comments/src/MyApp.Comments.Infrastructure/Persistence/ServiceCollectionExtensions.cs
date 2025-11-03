using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyApp.Comments.Infrastructure.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommentsPersistence(this IServiceCollection services, IConfiguration cfg)
    {
        var cs = cfg.GetConnectionString(Properties.Resources.DefaultConnection)
                 ?? throw new InvalidOperationException("Missing ConnectionStrings:DefaultConnection");
        services.AddDbContext<CommentsDbContext>(opt => opt.UseSqlServer(cs));
        return services;
    }
}
