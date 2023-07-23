using SocialNetwork.Tools.Cqs.Queries;

namespace SocialNetwork.Domain.Queries.Comment;

public class CommentUserIdListByPostQuery: IQuery<IEnumerable<int>>
{
    public int PostId { get; init; }

    public CommentUserIdListByPostQuery(int postId)
    {
        PostId = postId;
    }
}
