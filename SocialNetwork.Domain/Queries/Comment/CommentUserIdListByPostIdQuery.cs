using SocialNetwork.Tools.Cqs.Queries;

namespace SocialNetwork.Domain.Queries.Comment;

public class CommentUserIdListByPostIdQuery: IQuery<IEnumerable<int>>
{
    public int PostId { get; init; }

    public CommentUserIdListByPostIdQuery(int postId)
    {
        PostId = postId;
    }
}
