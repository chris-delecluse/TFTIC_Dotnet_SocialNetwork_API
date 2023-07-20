using SocialNetwork.Domain.Queries.Friend;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.Models;
using SocialNetwork.WebApi.Infrastructures.Security;

namespace SocialNetwork.WebApi.SignalR.Tools;

public abstract class FriendListHubTools
{
    private readonly IFriendRepository _friendService;

    protected FriendListHubTools(IFriendRepository friendService)
    {
        _friendService = friendService;
    }

    protected IEnumerable<FriendModel> GetUserFriendList(int id)
    {
        return _friendService.Execute(new FriendListByStateQuery(id, EFriendState.Accepted));
    }
}
