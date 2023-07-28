using MediatR;
using SocialNetwork.Domain.Queries.Queries.Comment;
using SocialNetwork.Domain.Repositories.Comment;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Handlers.Comment;

public class CommentListByPostQueryHandler : IRequestHandler<CommentListByPostQuery, IEnumerable<CommentModel>>
{
    private readonly ICommentRepository _commentRepository;
    
    public CommentListByPostQueryHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<IEnumerable<CommentModel>> Handle(CommentListByPostQuery request, CancellationToken cancellationToken)
    {
        return await _commentRepository.Find(request);
    }
}
