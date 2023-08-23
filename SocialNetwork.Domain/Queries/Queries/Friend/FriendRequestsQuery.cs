using MediatR;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Queries.Friend;

public class FriendRequestsQuery : IRequest<IEnumerable<FriendRequestModel>>
{
    public int RequestId { get; init; }

    public FriendRequestsQuery(int requestId)
    {
        RequestId = requestId;
    }
}
