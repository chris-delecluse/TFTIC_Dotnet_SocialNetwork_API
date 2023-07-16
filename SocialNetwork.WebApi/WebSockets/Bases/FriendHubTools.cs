using SocialNetwork.Domain.Queries.Friend;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.Models;
using SocialNetwork.WebApi.Infrastructures.Security;

namespace SocialNetwork.WebApi.WebSockets.Bases;

public abstract class FriendHubTools : GroupMessageHubTools
{
    private readonly IFriendRepository _friendService;

    protected FriendHubTools(IFriendRepository friendService)
    {
        _friendService = friendService;
    }

    protected IEnumerable<FriendEntity> GetUserFriendList(int id)
    {
        return _friendService.Execute(new FriendListByStateQuery(id, EFriendState.Accepted));
    }

    protected void ExecuteActionOnFriendList(UserInfo user, Action<FriendEntity, string> predicate)
    {
        foreach (FriendEntity friend in GetUserFriendList(user.Id))
        {
            predicate(friend, $"{user.FirstName} {user.LastName}");
        }
    }
}
