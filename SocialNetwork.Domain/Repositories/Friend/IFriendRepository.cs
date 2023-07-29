using SocialNetwork.Domain.Commands.Commands.Friend;
using SocialNetwork.Domain.Queries.Queries.Friend;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Repositories.Friend;

public interface IFriendRepository
{
    Task Insert(FriendCommand command);
    Task Update(UpdateFriendRequestCommand command);
    Task<IEnumerable<FriendModel>> Find(FriendListQuery query);
    Task<IEnumerable<FriendModel>> Find(FriendListByStateQuery query);
    Task Remove(RemoveFriendCommand command);
}
