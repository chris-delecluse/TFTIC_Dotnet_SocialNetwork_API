using MediatR;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Queries.Friend;

public class FriendListQuery:  IRequest<IEnumerable<FriendModel>>
{
    public int UserId { get; set; }

    public FriendListQuery(int userId)
    {
        UserId = userId;
    }
}
