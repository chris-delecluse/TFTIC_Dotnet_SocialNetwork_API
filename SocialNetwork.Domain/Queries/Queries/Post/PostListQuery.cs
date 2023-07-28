using MediatR;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Queries.Post;

public class PostListQuery : IRequest<IEnumerable<IGrouping<IPost, PostModel>>>
{
    public int Offset { get; init; }
    public int Limit { get; init; }
    public int IsDeleted { get; init; }

    public PostListQuery(int offset, int limit, bool isDeleted)
    {
        Offset = offset;
        Limit = limit;
        IsDeleted = isDeleted ? 1 : 0;
    }
}
