using SocialNetwork.Domain.Entities;
using SocialNetwork.Tools.Cqs.Queries;

namespace SocialNetwork.Domain.Queries.Post;

public class PostQuery : IQuery<PostEntity?>
{
    public int PostId { get; init; }
    public int UserId { get; init; }

    public PostQuery(int postId, int userId)
    {
        PostId = postId;
        UserId = userId;
    }
}
