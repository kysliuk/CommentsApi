using Microsoft.EntityFrameworkCore;
using MyApp.Comments.Core.Domain;

namespace MyApp.Comments.Infrastructure.Persistence;

public sealed class CommentsDbContext(DbContextOptions<CommentsDbContext> options) : DbContext(options)
{

    public DbSet<Comment> Comments => Set<Comment>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CommentsDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
