using SocialNetwork.Models;
using SocialNetwork.Tools.Cqs.Queries;

namespace SocialNetwork.Domain.Queries.Friend;

public class FriendListQuery: IQuery<IEnumerable<Models.FriendModel>>
{
    public int RequestId { get; init; }

    public FriendListQuery(int requestId)
    {
        RequestId = requestId;
    }
}
