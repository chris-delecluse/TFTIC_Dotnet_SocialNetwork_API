using Microsoft.AspNetCore.SignalR;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.Models;
using SocialNetwork.WebApi.Infrastructures.Security;
using SocialNetwork.WebApi.SignalR.Tools;
using SocialNetwork.WebApi.SignalR.Interfaces;
using SocialNetwork.WebApi.SignalR.Extensions;
using SocialNetwork.WebApi.SignalR.Hubs;

namespace SocialNetwork.WebApi.SignalR.Services;

public class AuthHubService : FriendListHubTools, IAuthHubService
{
    private readonly IHubContext<AuthHub, IClientHub> _authContext;

    public AuthHubService(IFriendRepository friendService, IHubContext<AuthHub, IClientHub> authContext) :
        base(friendService)
    {
        _authContext = authContext;
    }

    public void NotifyUserConnectedToFriends(UserEntity user)
    {
        foreach (FriendEntity friend in GetUserFriendList(user.Id))
        {
            string groupName = $"FriendsGroup_{friend.ResponderId}";
            _authContext?.AddToGroup(groupName);
            _authContext?.SendMessage(groupName, $"{user.FirstName} {user.LastName} has connected !");
        }
    }

    public void NotifyUserDisConnectedToFriends(UserInfo user)
    {
        foreach (FriendEntity friend in GetUserFriendList(user.Id))
        {
            string groupName = $"FriendsGroup_{friend.ResponderId}";
            _authContext?.SendMessage(groupName, $"{user.FirstName} {user.LastName} has disconnected !");
            _authContext?.RemoveToGroup(groupName);
        }
    }
}
