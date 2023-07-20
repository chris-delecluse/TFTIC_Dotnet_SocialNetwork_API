using SocialNetwork.Models;
using SocialNetwork.Tools.Cqs.Queries;

namespace SocialNetwork.Domain.Queries.Post;

public class AllPostDetailQuery : IQuery<IEnumerable<IGrouping<IComment, PostDetailModel>>>
{
    public int UserId { get; init; }

    public AllPostDetailQuery(int userId)
    {
        UserId = userId;
    }
}
