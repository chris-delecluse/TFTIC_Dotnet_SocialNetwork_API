using System.Text.Json;
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

    public async Task NotifyUserConnectedToFriends(UserModel userModel)
    {
        foreach (FriendModel friend in GetUserFriendList(userModel.Id))
        {
            string groupName = $"FriendsGroup_{friend.ResponderId}";
            await _authContext.AddToGroup(groupName);
            await _authContext.SendMessage(groupName,
                JsonSerializer.Serialize(new HubResponse("Connection",
                        $"{userModel.FirstName} {userModel.LastName} has connected !"
                    )
                )
            );
        }
    }

    public async Task NotifyUserDisConnectedToFriends(UserInfo user)
    {
        foreach (FriendModel friend in GetUserFriendList(user.Id))
        {
            string groupName = $"FriendsGroup_{friend.ResponderId}";
            await _authContext.SendMessage(groupName,
                JsonSerializer.Serialize(new HubResponse("DisConnection",
                        $"{user.FirstName} {user.LastName} has disconnected !"
                    )
                )
            );
            await _authContext.RemoveToGroup(groupName);
        }
    }
}
