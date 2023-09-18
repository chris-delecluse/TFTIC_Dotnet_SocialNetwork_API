using MediatR;
using SocialNetwork.Domain.Queries.Queries.Comment;
using SocialNetwork.Domain.Repositories.Comment;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Handlers.Comment;

public class CommentByIdQueryHandler: IRequestHandler<CommentByIdQuery, CommentModel?>
{
    private readonly ICommentRepository _commentRepository;
    
    public CommentByIdQueryHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<CommentModel?> Handle(CommentByIdQuery request, CancellationToken cancellationToken)
    {
        return await _commentRepository.Find(request);
    }
}
