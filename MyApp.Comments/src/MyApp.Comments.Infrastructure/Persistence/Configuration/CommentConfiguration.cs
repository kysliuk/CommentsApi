using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Comments.Core.Domain;

namespace MyApp.Comments.Infrastructure.Persistence;

public sealed class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> e)
    {
        e.ToTable("Comments");
        e.HasKey(x => x.Id);

        e.Property(x => x.UserName).IsRequired().HasMaxLength(50);
        e.Property(x => x.Email).IsRequired().HasMaxLength(100);
        e.Property(x => x.HomePage).HasMaxLength(200);
        e.Property(x => x.TextHtml).IsRequired().HasMaxLength(4000);

        e.Property(x => x.CreatedAtUtc).HasPrecision(0);

        e.Property(x => x.CreatedByUserId);
        e.Property(x => x.CreatedByName).HasMaxLength(100);

        e.HasIndex(x => x.CreatedAtUtc);
        e.HasIndex(x => x.UserName);
        e.HasIndex(x => x.Email);
    }
}
