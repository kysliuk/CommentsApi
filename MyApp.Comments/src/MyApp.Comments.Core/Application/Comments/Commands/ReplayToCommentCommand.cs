using MediatR;

namespace MyApp.Comments.Core.Application;

public sealed record ReplyToCommentCommand(
    Guid ParentId,
    string UserName, string Email, string? HomePage, string Html,
    Guid? CreatedByUserId, string? CreatedByName) : IRequest<CommentDto>;
