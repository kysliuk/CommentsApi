using MediatR;

namespace MyApp.Comments.Core.Application;

public sealed record GetThreadQuery(Guid RootId)
    : IRequest<IReadOnlyList<CommentDto>>;
