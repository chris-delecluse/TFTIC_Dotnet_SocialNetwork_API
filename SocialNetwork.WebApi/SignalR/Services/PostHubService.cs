using System.Text.Json;
using Microsoft.AspNetCore.SignalR;
using SocialNetwork.WebApi.SignalR.Hubs;
using SocialNetwork.WebApi.SignalR.Interfaces;
using SocialNetwork.WebApi.SignalR.TypedHubs;

namespace SocialNetwork.WebApi.SignalR.Services;

public class PostHubService : IPostHubService
{
    private readonly IHubContext<PostHub, IPostHub> _postHubContext;

    public PostHubService(IHubContext<PostHub, IPostHub> postHubContext) { _postHubContext = postHubContext; }

    public async Task NotifyNewPost<T>(T dataToSend)
    {
        await _postHubContext.Clients.All.ReceiveNewPost(JsonSerializer.Serialize(dataToSend));
    }

    public async Task NotifyPostLiked<T>(T dataToSend)
    {
        await _postHubContext.Clients.All.ReceiveLike(JsonSerializer.Serialize(dataToSend));
    }

    public async Task NotifyPostDisliked<T>(T dataToSend)
    {
        await _postHubContext.Clients.All.ReceiveDislike(JsonSerializer.Serialize(dataToSend));
    }

    public async Task NotifyPostComment<T>(T dataToSend)
    {
        await _postHubContext.Clients.All.ReceiveComment(JsonSerializer.Serialize(dataToSend));
    }
}
