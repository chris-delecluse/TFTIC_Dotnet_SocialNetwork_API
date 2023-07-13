using Microsoft.AspNetCore.SignalR;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Queries.Friend;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.WebApi.SignalR.Hubs;
using SocialNetwork.WebApi.SignalR.StronglyTypedHubs;

namespace SocialNetwork.WebApi.SignalR.Services.Auth;

public class AuthHubService : IAuthHubService
{
    private readonly IHubContext<AuthHub, IHub> _authHubContext;
    private readonly IFriendRepository _friendService;

    public AuthHubService(IHubContext<AuthHub, IHub> authHubContext, IFriendRepository friendService)
    {
        _authHubContext = authHubContext;
        _friendService = friendService;
    }

    public void NotifyUserConnectionToFriends(UserEntity user)
    {
        IEnumerable<FriendEntity> friendList = _friendService.Execute(
            new FriendListByStateQuery(user.Id, EFriendState.Accepted)
        );

        foreach (FriendEntity friend in friendList)
        {
            if (friend.ResponderId != user.Id)
            {
                string friendsGroup = $"FriendsGroup_{friend.ResponderId}";

                _authHubContext.Clients.Group(friendsGroup)
                    .AddToGroup(friendsGroup);

                _authHubContext.Clients.Group(friendsGroup)
                    .ReceiveMessage($"{user.FirstName} {user.Lastname} has connected.");
            }
        }
    }

    public void NotifyUserDisConnectionToFriends(int id, string firstName, string lastName)
    {
        IEnumerable<FriendEntity> friendList = _friendService.Execute(
            new FriendListByStateQuery(id, EFriendState.Accepted)
        );

        foreach (FriendEntity friend in friendList)
        {
            if (friend.ResponderId != id)
            {
                string friendsGroup = $"FriendsGroup_{friend.ResponderId}";

                _authHubContext.Clients.Group(friendsGroup)
                    .AddToGroup(friendsGroup);

                _authHubContext.Clients.Group(friendsGroup)
                    .ReceiveMessage($"{firstName} {lastName} has disconnected.");
            }
        }
    }
}
