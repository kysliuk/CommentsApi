using MyApp.Comments.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Comments.Core.Application.Comments;

public sealed class CommentService(IUnitOfWork uow) : ICommentService
{
    public async Task<CommentDto> CreateForTargetAsync(
        string targetType, Guid targetId,
        string userName, string email, string? homePage, string html,
        Guid? createdByUserId, string? createdByName, CancellationToken ct)
    {
        var entity = Comment.CreateForTarget(targetType, targetId, userName, email, homePage, html,
                                             createdByUserId, createdByName);

        await uow.Repository<Comment>().AddAsync(entity, ct);
        await uow.SaveChangesAsync(ct);
        return Map(entity);
    }

    public async Task<CommentDto> ReplyAsync(
        Guid parentId, string userName, string email, string? homePage, string html,
        Guid? createdByUserId, string? createdByName, CancellationToken ct)
    {
        var parent = await uow.Repository<Comment>().GetByIdAsync(parentId, ct);
        var entity = Comment.ReplyTo(parent, userName, email, homePage, html, createdByUserId, createdByName);

        await uow.Repository<Comment>().AddAsync(entity, ct);
        await uow.SaveChangesAsync(ct);
        return Map(entity);
    }

    public async Task<IReadOnlyList<CommentDto>> GetPageForTargetAsync(
        string targetType, Guid targetId, int page, int size, bool asc, CancellationToken ct)
    {
        if (page < 1) page = 1;
        if (size < 1) size = 25;

        var q = uow.Repository<Comment>().All()
            .Where(c => c.TargetType == targetType && c.TargetId == targetId);

        q = asc ? q.OrderBy(c => c.CreatedAt) : q.OrderByDescending(c => c.CreatedAt);

        var items = await q.Skip((page - 1) * size).Take(size).ToListAsync(ct);
        return items.Select(Map).ToList();
    }

    public async Task<IReadOnlyList<CommentDto>> GetThreadAsync(Guid rootId, CancellationToken ct)
    {
        var items = await uow.Repository<Comment>().All()
            .Where(c => c.RootId == rootId)
            .OrderBy(c => c.CreatedAt)
            .ToListAsync(ct);

        return items.Select(Map).ToList();
    }

    private static CommentDto Map(Comment c) =>
        new(c.Id, c.TargetType, c.TargetId, c.ParentId, c.RootId, c.Depth,
            c.UserName, c.Email, c.CreatedAt, c.TextHtml);
}
