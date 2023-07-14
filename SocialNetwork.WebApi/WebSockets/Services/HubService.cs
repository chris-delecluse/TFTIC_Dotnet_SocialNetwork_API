using Microsoft.AspNetCore.SignalR;
using SocialNetwork.Domain.Queries.Friend;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.Models;
using SocialNetwork.WebApi.WebSockets.StronglyTypedHubs;
using SocialNetwork.WebApi.WebSockets.Hubs;

namespace SocialNetwork.WebApi.WebSockets.Services;

public class HubService : IHubService
{
    private readonly IHubContext<AuthHub, IHub> _authHub;
    private readonly IHubContext<ChatHub, IHub> _chatHub;
    private readonly IHubContext<CommentHub, IHub> _commentHub;
    private readonly IHubContext<LikeHub, IHub> _likeHub;
    private readonly IHubContext<PostHub, IHub> _postHub;
    private readonly IFriendRepository _friendService;

    public HubService(
        IHubContext<AuthHub, IHub> authHub,
        IHubContext<ChatHub, IHub> chatHub,
        IHubContext<CommentHub, IHub> commentHub,
        IHubContext<LikeHub, IHub> likeHub,
        IHubContext<PostHub, IHub> postHub,
        IFriendRepository friendService
    )
    {
        _authHub = authHub;
        _chatHub = chatHub;
        _commentHub = commentHub;
        _likeHub = likeHub;
        _postHub = postHub;
        _friendService = friendService;
    }

    public void NotifyUserConnectionToFriends(UserEntity user)
    {
        foreach (FriendEntity friend in GetUserFriendList(user.Id))
        {
            if (friend.ResponderId != user.Id)
            {
                NotifyGroup("FriendsGroup",
                    friend.ResponderId,
                    $"{user.FirstName} {user.Lastname} has connected.",
                    _authHub
                );
            }
        }
    }

    public void NotifyUserDisConnectionToFriends(int id, string firstName, string lastName)
    {
        foreach (FriendEntity friend in GetUserFriendList(id))
        {
            if (friend.ResponderId != id)
            {
                NotifyGroup("FriendsGroup",
                    friend.ResponderId,
                    $"{firstName} {lastName} has disconnected.",
                    _authHub
                );
            }
        }
    }

    private IEnumerable<FriendEntity> GetUserFriendList(int id) =>
        _friendService.Execute(new FriendListByStateQuery(id, EFriendState.Accepted));

    private void NotifyGroup<THub>(string groupName, string message, IHubContext<THub, IHub> hubContext)
        where THub : Hub<IHub>
    {
        hubContext.Clients.Group(groupName)
            .JoinGroup(groupName);

        hubContext.Clients.Group(groupName)
            .ReceiveMessage(message);
    }

    private void NotifyGroup<THub>(string groupName, int targetId, string message, IHubContext<THub, IHub> hubContext)
        where THub : Hub<IHub> =>
        NotifyGroup($"{groupName}_{targetId}", message, hubContext);
}
