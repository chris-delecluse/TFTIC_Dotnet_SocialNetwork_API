using SocialNetwork.Models;
using SocialNetwork.Tools.Cqs.Queries;

namespace SocialNetwork.Domain.Queries.Friend;

public class FriendListByStateQuery : IQuery<IEnumerable<FriendEntity>>
{
    public int RequestId { get; init; }
    public EFriendState State { get; init; }

    public FriendListByStateQuery(int requestId, EFriendState state)
    {
        RequestId = requestId;
        State = state;
    }
}
