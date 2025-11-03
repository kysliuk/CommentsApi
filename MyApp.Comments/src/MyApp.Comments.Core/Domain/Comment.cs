namespace MyApp.Comments.Core.Domain;

public sealed class Comment
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid? ParentId { get; private set; }

    public string UserName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string? HomePage { get; private set; }

    public string TextHtml { get; private set; } = null!;

    public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;

    public Guid? CreatedByUserId { get; private set; }
    public string? CreatedByName { get; private set; }

    private Comment() { }

    public static Comment Create(
        string userName, string email, string? homePage, string textHtml, Guid? parentId = null,
        Guid? createdByUserId = null, string? createdByName = null)
        => new()
        {
            UserName = userName,
            Email = email,
            HomePage = homePage,
            TextHtml = textHtml,
            ParentId = parentId,
            CreatedByUserId = createdByUserId,
            CreatedByName = createdByName
        };
}
