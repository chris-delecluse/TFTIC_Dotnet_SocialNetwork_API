using MediatR;
using SocialNetwork.Domain.Queries.Queries.Comment;
using SocialNetwork.Domain.Repositories.Comment;

namespace SocialNetwork.Domain.Queries.Handlers.Comment;

public class CommentUserIdListByPostQueryHandler : IRequestHandler<CommentUserIdListByPostQuery, IEnumerable<int>>
{
    private readonly ICommentRepository _commentRepository;
    
    public CommentUserIdListByPostQueryHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<IEnumerable<int>> Handle(CommentUserIdListByPostQuery request, CancellationToken cancellationToken)
    {
        return await _commentRepository.Find(request);
    }
}
