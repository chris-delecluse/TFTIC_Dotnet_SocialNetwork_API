using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using SocialNetwork.Models;
using SocialNetwork.WebApi.Models.Models;
using SocialNetwork.WebApi.SignalR.Extensions;
using SocialNetwork.WebApi.SignalR.Hubs;
using SocialNetwork.WebApi.SignalR.Tools;
using SocialNetwork.WebApi.SignalR.Interfaces;

namespace SocialNetwork.WebApi.SignalR.Services;

public class PostHubService : FriendManager, IPostHubService
{
    private readonly IHubContext<PostHub, IClientHub> _postHubContext;
    private readonly IHubContext<LikeHub, IClientHub> _likeHubContext;

    public PostHubService(
        IMediator mediator,
        IHubContext<PostHub, IClientHub> postHubContext,
        IHubContext<LikeHub, IClientHub> likeHubContext
    ) :
        base(mediator)
    {
        _postHubContext = postHubContext;
        _likeHubContext = likeHubContext;
    }

    public async Task NotifyNewPostToFriends<T>(TokenUserInfo tokenUser, T dataToSend)
    {
        foreach (FriendRequestModel friend in await GetUserFriendList(tokenUser.Id))
        {
            string groupName = $"PostGroup_{friend.ResponderId}";
            await _postHubContext.AddToGroup(groupName);
            await _postHubContext.SendMessage(groupName, JsonSerializer.Serialize(dataToSend));
        }
    }

    public async Task NotifyLikeToPost<T>(int postId, T dataToSend)
    {
        string groupName = $"LikeGroup_{postId}";
        await _likeHubContext.AddToGroup(groupName);
        await _likeHubContext.SendMessage(groupName, JsonSerializer.Serialize(dataToSend));
    }
}
