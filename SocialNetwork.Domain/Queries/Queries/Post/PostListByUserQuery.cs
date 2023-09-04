using MediatR;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Queries.Post;

public class PostListByUserQuery : IRequest<IEnumerable<IGrouping<IPost, PostModel>>>
{
    public int UserId { get; init; }
    public int Offset { get; init; }
    public int Limit { get; init; }
    public int IsDeleted { get; init; }

    public PostListByUserQuery(int userId, int offset, int limit, bool isDeleted)
    {
        UserId = userId;
        Offset = offset;
        Limit = limit;
        IsDeleted = isDeleted ? 1 : 0;
    }
}
