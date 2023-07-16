using Microsoft.AspNetCore.SignalR;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.WebApi.Infrastructures.Security;
using SocialNetwork.WebApi.WebSockets.Bases;
using SocialNetwork.WebApi.WebSockets.Hubs;
using SocialNetwork.WebApi.WebSockets.Interfaces;

namespace SocialNetwork.WebApi.WebSockets.Services;

public class PostHubService : HubTools, IPostHubService
{
    private readonly IHubContext<PostHub, IBaseHub> _hubContext;

    public PostHubService(IFriendRepository friendService, IHubContext<PostHub, IBaseHub> hubContext) :
        base(friendService)
    {
        _hubContext = hubContext;
    }

    public void NotifyNewPostToFriends(UserInfo user)
    {
        ExecuteActionOnFriendList(user,
            (friend, userFullName) =>
                SendGroupMessage("PostGroup", friend.ResponderId, $"{userFullName} has added a new post.", _hubContext)
        );
    }
}
