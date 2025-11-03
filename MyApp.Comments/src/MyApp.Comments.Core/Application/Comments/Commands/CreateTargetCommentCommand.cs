using MediatR;

namespace MyApp.Comments.Core.Application;

public sealed record CreateTargetCommentCommand(
    string TargetType, Guid TargetId,
    string UserName, string Email, string? HomePage, string Html,
    Guid? CreatedByUserId, string? CreatedByName) : IRequest<CommentDto>;
