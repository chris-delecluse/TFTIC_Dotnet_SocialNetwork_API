using MediatR;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Queries.Post;

public class PostQuery : IRequest<IEnumerable<IGrouping<IPost, PostModel>>>
{
    public int PostId { get; init; }
    public int IsDeleted { get; init; }

    public PostQuery(int postId, bool isDeleted)
    {
        PostId = postId;
        IsDeleted = isDeleted ? 1 : 0;
    }
}
