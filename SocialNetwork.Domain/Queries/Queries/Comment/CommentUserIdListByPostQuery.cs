using MediatR;

namespace SocialNetwork.Domain.Queries.Queries.Comment;

public class CommentUserIdListByPostQuery : IRequest<IEnumerable<int>>
{
    public int PostId { get; init; }

    public CommentUserIdListByPostQuery(int postId)
    {
        PostId = postId;
    }
}
