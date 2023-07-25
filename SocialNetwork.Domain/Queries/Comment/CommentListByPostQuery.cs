using SocialNetwork.Models;
using SocialNetwork.Tools.Cqs.Queries;

namespace SocialNetwork.Domain.Queries.Comment;

public class CommentListByPostQuery: IQuery<IEnumerable<CommentModel>>
{
    public int PostId { get; init; }

    public CommentListByPostQuery(int postId)
    {
        PostId = postId;
    }
}
