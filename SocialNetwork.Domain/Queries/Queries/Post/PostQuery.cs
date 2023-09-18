using MediatR;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Queries.Post;

public class PostQuery : IRequest<IEnumerable<IGrouping<IPost, PostModel>>>
{
    public int Id { get; init; }
    public int IsDeleted { get; init; }

    public PostQuery(int postId, bool isDeleted)
    {
        Id = postId;
        IsDeleted = isDeleted ? 1 : 0;
    }
}
