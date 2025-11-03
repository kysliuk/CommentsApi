namespace MyApp.Comments.Core.Application;

public sealed record CommentDto(
    Guid Id,
    string TargetType,
    Guid TargetId,
    Guid? ParentId,
    Guid RootId,
    byte Depth,
    string UserName,
    string Email,
    DateTime CreatedAt,
    string Html);
