using MediatR;

namespace MyApp.Comments.Core.Application;

public sealed record GetTargetCommentsQuery(
    string TargetType, Guid TargetId, int Page = 1, int Size = 25, bool Asc = false)
    : IRequest<IReadOnlyList<CommentDto>>;
