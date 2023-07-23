using SocialNetwork.Models;
using SocialNetwork.Tools.Cqs.Queries;

namespace SocialNetwork.Domain.Queries.Post;

public class PostQuery : IQuery<IEnumerable<IGrouping<IPost, PostModel>>>
{
    public int PostId { get; init; }

    public PostQuery(int postId)
    {
        PostId = postId;
    }
}
