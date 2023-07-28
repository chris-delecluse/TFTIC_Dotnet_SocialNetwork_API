using SocialNetwork.Domain.Commands.Commands.Friend;
using SocialNetwork.Domain.Queries.Queries.Friend;
using SocialNetwork.Models;
using SocialNetwork.Tools.Cqs.Shared;

namespace SocialNetwork.Domain.Repositories.Friend;

public interface IFriendRepository
{
    Task<ICommandResult> Insert(FriendCommand command);
    Task<ICommandResult> Update(UpdateFriendRequestCommand command);
    Task<IEnumerable<FriendModel>> Find(FriendListQuery query);
    Task<IEnumerable<FriendModel>> Find(FriendListByStateQuery query);
    Task<ICommandResult> Remove(RemoveFriendCommand command);
}
