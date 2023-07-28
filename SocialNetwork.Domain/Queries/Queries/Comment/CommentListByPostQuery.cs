using MediatR;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Queries.Comment;

public class CommentListByPostQuery : IRequest<IEnumerable<CommentModel>>
{
    public int PostId { get; init; }

    public CommentListByPostQuery(int postId)
    {
        PostId = postId;
    }
}
