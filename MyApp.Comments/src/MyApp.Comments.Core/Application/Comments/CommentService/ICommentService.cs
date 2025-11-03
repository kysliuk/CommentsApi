namespace MyApp.Comments.Core.Application;

public interface ICommentService
{
    Task<CommentDto> CreateForTargetAsync(string targetType, Guid targetId,
        string userName, string email, string? homePage, string html,
        Guid? createdByUserId, string? createdByName, CancellationToken ct);

    Task<CommentDto> ReplyAsync(Guid parentId,
        string userName, string email, string? homePage, string html,
        Guid? createdByUserId, string? createdByName, CancellationToken ct);

    Task<IReadOnlyList<CommentDto>> GetPageForTargetAsync(
        string targetType, Guid targetId, int page, int size, bool asc, CancellationToken ct);

    Task<IReadOnlyList<CommentDto>> GetThreadAsync(Guid rootId, CancellationToken ct);
}
