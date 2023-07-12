using SocialNetwork.Domain.Entities;
using SocialNetwork.Tools.Cqs.Queries;

namespace SocialNetwork.Domain.Queries.Friend;

public class FriendListQuery: IQuery<IEnumerable<FriendEntity>>
{
    public int RequestId { get; init; }

    public FriendListQuery(int requestId)
    {
        RequestId = requestId;
    }
}
