using SocialNetwork.Models;
using SocialNetwork.Tools.Cqs.Queries;

namespace SocialNetwork.Domain.Queries.Comment;

public class CommentGroupByPostIdQuery: IQuery<IEnumerable<IGrouping<IPost, CommentModel>>> 
{
    public int PostId { get; init; }

    public CommentGroupByPostIdQuery(int postId)
    {
        PostId = postId;
    }
}
