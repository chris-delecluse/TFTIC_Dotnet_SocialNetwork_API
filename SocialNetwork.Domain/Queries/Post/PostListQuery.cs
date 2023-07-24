using SocialNetwork.Models;
using SocialNetwork.Tools.Cqs.Queries;

namespace SocialNetwork.Domain.Queries.Post;

public class PostListQuery : IQuery<IEnumerable<IGrouping<IPost, PostModel>>>
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
