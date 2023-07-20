using SocialNetwork.Models;
using SocialNetwork.Tools.Cqs.Queries;

namespace SocialNetwork.Domain.Queries.Post;

public class PostQuery : IQuery<Models.PostModel?>
{
    public int PostId { get; init; }
    public int UserId { get; init; }

    public PostQuery(int postId, int userId)
    {
        PostId = postId;
        UserId = userId;
    }
}
