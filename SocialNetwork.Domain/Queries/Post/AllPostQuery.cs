using SocialNetwork.Tools.Cqs.Queries;

namespace SocialNetwork.Domain.Queries.Post;

public class AllPostQuery: IQuery<IEnumerable<Models.PostModel>>
{
    public int UserId { get; init; }

    public AllPostQuery(int userId)
    {
        UserId = userId;
    }
}
