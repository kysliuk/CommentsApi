using Microsoft.Extensions.DependencyInjection;
using MyApp.Comments.Core.Application;
using MyApp.Comments.Core.Application.Comments;

namespace MyApp.Comments.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Domain.AssemblyMarker).Assembly));
            services.AddAutoMapper(typeof(Domain.AssemblyMarker));

            services.AddScoped<ICommentService, CommentService>();

            return services;
        }
    }
}
