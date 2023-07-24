using SocialNetwork.Models;
using SocialNetwork.Tools.Cqs.Queries;

namespace SocialNetwork.Domain.Queries.Post;

public class PostQuery : IQuery<IEnumerable<IGrouping<IPost, PostModel>>>
{
    public int PostId { get; init; }
    public int IsDeleted { get; init; }

    public PostQuery(int postId, bool isDeleted)
    {
        PostId = postId;
        IsDeleted = isDeleted ? 1 : 0;
    }
}
