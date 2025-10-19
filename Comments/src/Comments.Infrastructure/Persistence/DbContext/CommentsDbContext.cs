using Microsoft.EntityFrameworkCore;

namespace Comments.Infrastructure;

public class CommentsDbContext(DbContextOptions<CommentsDbContext> options) : DbContext(options)
{
}
