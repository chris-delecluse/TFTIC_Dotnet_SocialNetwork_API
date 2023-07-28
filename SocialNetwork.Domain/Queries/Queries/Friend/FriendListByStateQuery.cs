using MediatR;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Queries.Friend;

public class FriendListByStateQuery : IRequest<IEnumerable<FriendModel>>
{
    public int RequestId { get; init; }
    public EFriendState State { get; init; }

    public FriendListByStateQuery(int requestId, EFriendState state)
    {
        RequestId = requestId;
        State = state;
    }
}
