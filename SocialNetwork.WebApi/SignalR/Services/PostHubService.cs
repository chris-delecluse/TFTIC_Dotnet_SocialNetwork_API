using System.Text.Json;
using Microsoft.AspNetCore.SignalR;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.Models;
using SocialNetwork.WebApi.Infrastructures.Security;
using SocialNetwork.WebApi.SignalR.Extensions;
using SocialNetwork.WebApi.SignalR.Hubs;
using SocialNetwork.WebApi.SignalR.Tools;
using SocialNetwork.WebApi.SignalR.Interfaces;

namespace SocialNetwork.WebApi.SignalR.Services;

public class PostHubService : FriendListHubTools, IPostHubService
{
    private readonly IHubContext<PostHub, IClientHub> _hubContext;

    public PostHubService(IFriendRepository friendService, IHubContext<PostHub, IClientHub> hubContext) :
        base(friendService)
    {
        _hubContext = hubContext;
    }

    public async Task NotifyNewPostToFriends<T>(UserInfo user, T dataToSend)
    {
        foreach (FriendModel friend in GetUserFriendList(user.Id))
        {
            string groupName = $"PostGroup_{friend.ResponderId}";
            await _hubContext.AddToGroup(groupName);
            await _hubContext.SendMessage(groupName, JsonSerializer.Serialize(dataToSend));
        }
    }
}
