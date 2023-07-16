using Microsoft.AspNetCore.SignalR;
using SocialNetwork.Domain.Queries.Friend;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.Models;
using SocialNetwork.WebApi.Infrastructures.Security;

namespace SocialNetwork.WebApi.WebSockets.Bases;

public abstract class HubTools
{
    private readonly IFriendRepository _friendService;

    protected HubTools(IFriendRepository friendService)
    {
        _friendService = friendService;
    }

    protected IEnumerable<FriendEntity> GetUserFriendList(int id)
    {
        return _friendService.Execute(new FriendListByStateQuery(id, EFriendState.Accepted));
    }
    
    protected void SendGroupMessage<THub>(string groupName, int targetId, string message, IHubContext<THub, IBaseHub> hubContext)
        where THub : Hub<IBaseHub>
    {
        SendGroupMessage($"{groupName}_{targetId}", message, hubContext);
    }

    protected void SendGroupMessage<THub>(string groupName, string message, IHubContext<THub, IBaseHub> hubContext)
        where THub : Hub<IBaseHub>
    {
        hubContext.Clients.Group(groupName)
            .JoinGroup(groupName);

        hubContext.Clients.Group(groupName)
            .ReceiveMessage(message);
    }

    protected void ExecuteActionOnFriendList(UserInfo user, Action<FriendEntity, string> predicate)
    {
        foreach (FriendEntity friend in GetUserFriendList(user.Id))
        {
            predicate(friend, $"{user.FirstName} {user.LastName}");
        }
    }
}
