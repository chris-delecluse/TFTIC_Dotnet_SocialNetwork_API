using SocialNetwork.Models;
using SocialNetwork.Tools.Cqs.Queries;

namespace SocialNetwork.Domain.Queries.Post;

public class AllPostQuery: IQuery<IEnumerable<PostEntity>>
{
    public int UserId { get; init; }

    public AllPostQuery(int userId)
    {
        UserId = userId;
    }
}
