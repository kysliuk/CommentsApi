namespace MyApp.Comments.Core.Application;

public sealed class CreateTargetCommentHandler(ICommentService service) : CommentHandlerBase<CreateTargetCommentCommand, CommentDto>(service)
{
    public override async Task<CommentDto> Handle(CreateTargetCommentCommand r, CancellationToken ct) =>
        await _service.CreateForTargetAsync(r.TargetType, r.TargetId, r.UserName, r.Email, r.HomePage, r.Html,
                                 r.CreatedByUserId, r.CreatedByName, ct);

}

