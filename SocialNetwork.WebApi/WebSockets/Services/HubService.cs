using Microsoft.AspNetCore.SignalR;
using SocialNetwork.Domain.Queries.Friend;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.Models;
using SocialNetwork.WebApi.Infrastructures.Security;
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

    public void NotifyUserConnectedToFriends(UserEntity user)
    {
        ExecuteActionOnFriendList(new UserInfo(user.Id, user.FirstName, user.LastName),
            (friend, userFullName) => SendGroupMessage("FriendsGroup",
                friend.ResponderId,
                $"{userFullName} has connected.",
                _authHub
            )
        );
    }

    public void NotifyUserDisConnectedToFriends(UserInfo user)
    {
        ExecuteActionOnFriendList(user,
            (friend, userFullName) => SendGroupMessage("FriendsGroup",
                friend.ResponderId,
                $"{userFullName} has disconnected.",
                _authHub
            )
        );
    }

    public void NotifyNewPostToFriends(UserInfo user, int postId)
    {
        throw new NotImplementedException();
    }

    private void SendGroupMessage<THub>(string groupName, int targetId, string message, IHubContext<THub, IHub> hubContext)
        where THub : Hub<IHub>
    {
        SendGroupMessage($"{groupName}_{targetId}", message, hubContext);
    }

    private void SendGroupMessage<THub>(string groupName, string message, IHubContext<THub, IHub> hubContext)
        where THub : Hub<IHub>
    {
        hubContext.Clients.Group(groupName)
            .JoinGroup(groupName);

        hubContext.Clients.Group(groupName)
            .ReceiveMessage(message);
    }

    private IEnumerable<FriendEntity> GetUserFriendList(int id)
    {
        return _friendService.Execute(new FriendListByStateQuery(id, EFriendState.Accepted));
    }

    private void ExecuteActionOnFriendList(UserInfo user, Action<FriendEntity, string> predicate)
    {
        foreach (FriendEntity friend in GetUserFriendList(user.Id))
        {
            predicate(friend, $"{user.FirstName} {user.LastName}");
        }
    }
}
