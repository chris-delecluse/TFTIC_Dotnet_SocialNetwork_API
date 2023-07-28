using MediatR;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Queries.Friend;

public class FriendListQuery : IRequest<IEnumerable<FriendModel>>
{
    public int RequestId { get; init; }

    public FriendListQuery(int requestId)
    {
        RequestId = requestId;
    }
}
