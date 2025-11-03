namespace MyApp.Comments.Core.Application;

public sealed class GetThreadHandler(ICommentService service) : CommentHandlerBase<GetThreadQuery, IReadOnlyList<CommentDto>>(service)
{
    public override async Task<IReadOnlyList<CommentDto>> Handle(GetThreadQuery q, CancellationToken ct) =>
        await _service.GetThreadAsync(q.RootId, ct);
}
