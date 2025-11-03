namespace MyApp.Comments.Core.Application;

public sealed class GetTargetCommentsHandler(ICommentService service) : CommentHandlerBase<GetTargetCommentsQuery, IReadOnlyList<CommentDto>>(service)
{
    public override async Task<IReadOnlyList<CommentDto>> Handle(GetTargetCommentsQuery q, CancellationToken ct) =>
        await _service.GetPageForTargetAsync(q.TargetType, q.TargetId, q.Page, q.Size, q.Asc, ct);
}
