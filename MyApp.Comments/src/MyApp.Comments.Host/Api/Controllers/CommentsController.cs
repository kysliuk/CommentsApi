using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyApp.Comments.Core.Application;

namespace MyApp.Comments.Host.Api;

[ApiController]
[Route("api/[controller]")]
public sealed class CommentsController(IMediator mediator) : ControllerBase
{
    [HttpPost("{targetType}/{targetId:guid}")]
    public async Task<ActionResult<CommentDto>> CreateForTarget(
        string targetType, Guid targetId, [FromBody] CreateRequest body, CancellationToken ct)
    {
        var dto = await mediator.Send(new CreateTargetCommentCommand(
            targetType, targetId, body.UserName, body.Email, body.HomePage, body.Html,
            body.CreatedByUserId, body.CreatedByName), ct);

        return CreatedAtAction(nameof(GetForTarget), new { targetType, targetId, page = 1 }, dto);
    }

    [HttpPost("{parentId:guid}/reply")]
    public async Task<ActionResult<CommentDto>> Reply(Guid parentId, [FromBody] CreateRequest body, CancellationToken ct)
    {
        var dto = await mediator.Send(new ReplyToCommentCommand(
            parentId, body.UserName, body.Email, body.HomePage, body.Html,
            body.CreatedByUserId, body.CreatedByName), ct);

        return Ok(dto);
    }

    [HttpGet("{targetType}/{targetId:guid}")]
    public Task<IReadOnlyList<CommentDto>> GetForTarget(
        string targetType, Guid targetId, [FromQuery] int page = 1, [FromQuery] int size = 25, [FromQuery] bool asc = false,
        CancellationToken ct = default) =>
        mediator.Send(new GetTargetCommentsQuery(targetType, targetId, page, size, asc), ct);

    [HttpGet("thread/{rootId:guid}")]
    public Task<IReadOnlyList<CommentDto>> GetThread(Guid rootId, CancellationToken ct) =>
        mediator.Send(new GetThreadQuery(rootId), ct);

    public sealed record CreateRequest(string UserName, string Email, string? HomePage, string Html,
                                       Guid? CreatedByUserId, string? CreatedByName);
}
