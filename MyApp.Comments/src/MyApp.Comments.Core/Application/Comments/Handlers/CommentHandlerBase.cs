using MediatR;

namespace MyApp.Comments.Core.Application;

public abstract class CommentHandlerBase<TRequset, TResponse>(ICommentService service) : IRequestHandler<TRequset, TResponse> 
    where TRequset : IRequest<TResponse>
{
    protected readonly ICommentService _service = service;
    public abstract Task<TResponse> Handle(TRequset request, CancellationToken cancellationToken);
}
