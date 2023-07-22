using SocialNetwork.Tools.Cqs.Queries;

namespace SocialNetwork.Domain.Queries.Comment;

public class CommentsUserIdByPostIdQuery: IQuery<IEnumerable<int>>
{
    public int PostId { get; init; }

    public CommentsUserIdByPostIdQuery(int postId)
    {
        PostId = postId;
    }
}
