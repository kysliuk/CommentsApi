namespace MyApp.Comments.Core.Application;

public sealed class ReplyToCommentHandler(ICommentService service) : CommentHandlerBase<ReplyToCommentCommand, CommentDto>(service)
{
    public override async Task<CommentDto> Handle(ReplyToCommentCommand r, CancellationToken ct) =>
        await _service.ReplyAsync(r.ParentId, r.UserName, r.Email, r.HomePage, r.Html,
                       r.CreatedByUserId, r.CreatedByName, ct);
}
