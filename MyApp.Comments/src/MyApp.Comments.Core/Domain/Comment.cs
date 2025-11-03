namespace MyApp.Comments.Core.Domain;

public sealed class Comment : BaseEntity
{
    public string TargetType { get; private set; } = null!;
    public Guid TargetId { get; private set; }

    public Guid? ParentId { get; private set; }
    public Guid RootId { get; private set; }
    public byte Depth { get; private set; }

    public string UserName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string? HomePage { get; private set; }
    public string TextHtml { get; private set; } = null!;


    public Guid? CreatedByUserId { get; private set; }
    public string? CreatedByName { get; private set; }

    private Comment() { }

    public static Comment CreateForTarget(
        string targetType, Guid targetId,
        string userName, string email, string? homePage, string textHtml,
        Guid? createdByUserId = null, string? createdByName = null)
    {
        if (string.IsNullOrWhiteSpace(targetType))
            throw new ArgumentException("Target type is required.", nameof(targetType));
        if (targetId == Guid.Empty)
            throw new ArgumentException("Target id must be non-empty.", nameof(targetId));

        var c = new Comment
        {
            TargetType = targetType.Trim(),
            TargetId = targetId,
            ParentId = null,
            UserName = userName,
            Email = email,
            HomePage = homePage,
            TextHtml = textHtml,
            CreatedByUserId = createdByUserId,
            CreatedByName = createdByName,
            Depth = 0
        };
        c.RootId = c.Id;
        return c;
    }

    public static Comment ReplyTo(
        Comment parent,
        string userName, string email, string? homePage, string textHtml,
        Guid? createdByUserId = null, string? createdByName = null)
    {
        ArgumentNullException.ThrowIfNull(parent);

        var c = new Comment
        {
            TargetType = parent.TargetType,
            TargetId = parent.TargetId,
            ParentId = parent.Id,
            RootId = parent.RootId,
            Depth = (byte)(parent.Depth + 1),
            UserName = userName,
            Email = email,
            HomePage = homePage,
            TextHtml = textHtml,
            CreatedByUserId = createdByUserId,
            CreatedByName = createdByName
        };
        return c;
    }
}
