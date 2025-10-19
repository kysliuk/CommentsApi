using Microsoft.EntityFrameworkCore;

namespace Comments.Infrastructure;

public class AuthDbContext(DbContextOptions<AuthDbContext> options) : DbContext(options)
{
}
