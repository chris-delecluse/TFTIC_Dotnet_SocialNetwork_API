using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using SocialNetwork.Models;
using SocialNetwork.WebApi.Models.Models;
using SocialNetwork.WebApi.SignalR.Tools;
using SocialNetwork.WebApi.SignalR.Interfaces;
using SocialNetwork.WebApi.SignalR.Extensions;
using SocialNetwork.WebApi.SignalR.Hubs;

namespace SocialNetwork.WebApi.SignalR.Services;

public class AuthHubService : FriendListHubTools, IAuthHubService
{
    private readonly IHubContext<AuthHub, IClientHub> _authContext;

    public AuthHubService(IMediator mediator, IHubContext<AuthHub, IClientHub> authContext) :
        base(mediator)
    {
        _authContext = authContext;
    }

    public async Task NotifyUserConnectedToFriends(UserModel userModel)
    {
        foreach (FriendModel friend in await GetUserFriendList(userModel.Id))
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

    public async Task NotifyUserDisConnectedToFriends(TokenUserInfo tokenUser)
    {
        foreach (FriendModel friend in await GetUserFriendList(tokenUser.Id))
        {
            string groupName = $"FriendsGroup_{friend.ResponderId}";
            await _authContext.SendMessage(groupName,
                JsonSerializer.Serialize(new HubResponse("DisConnection",
                        $"{tokenUser.FirstName} {tokenUser.LastName} has disconnected !"
                    )
                )
            );
            await _authContext.RemoveToGroup(groupName);
        }
    }
}
