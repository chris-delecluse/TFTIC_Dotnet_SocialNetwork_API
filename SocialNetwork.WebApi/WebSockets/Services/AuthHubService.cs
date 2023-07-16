using Microsoft.AspNetCore.SignalR;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.Models;
using SocialNetwork.WebApi.Infrastructures.Security;
using SocialNetwork.WebApi.WebSockets.Bases;
using SocialNetwork.WebApi.WebSockets.Hubs;
using SocialNetwork.WebApi.WebSockets.Interfaces;

namespace SocialNetwork.WebApi.WebSockets.Services;

public class AuthHubService : FriendHubTools, IAuthHubService
{
    private readonly IHubContext<AuthHub, IBaseHub> _authContext;

    public AuthHubService(IFriendRepository friendService, IHubContext<AuthHub, IBaseHub> authContext) :
        base(friendService)
    {
        _authContext = authContext;
    }

    public void NotifyUserConnectedToFriends(UserEntity user)
    {
        ExecuteActionOnFriendList(new UserInfo(user.Id, user.FirstName, user.LastName),
            (friend, userFullName) =>
                SendGroupMessage("FriendsGroup", friend.ResponderId, $"{userFullName} has connected.", _authContext)
        );
    }

    public void NotifyUserDisConnectedToFriends(UserInfo user)
    {
        ExecuteActionOnFriendList(user,
            (friend, userFullName) =>
                SendGroupMessage("FriendsGroup", friend.ResponderId, $"{userFullName} has disconnected.", _authContext)
        );
    }
}
