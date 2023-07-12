using Microsoft.AspNetCore.SignalR;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Queries.Friend;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.WebApi.Infrastructures.SignalR.Hubs;
using SocialNetwork.WebApi.Infrastructures.SignalR.StronglyTypedHubs;

namespace SocialNetwork.WebApi.Infrastructures.SignalR;

public class HubService : IHubService
{
    private readonly IHubContext<AuthHub, IHub> _authHubContext;

    public HubService(IHubContext<AuthHub, IHub> authHubContext)
    {
        _authHubContext = authHubContext;
    }

    public void NotifyUserConnectionToFriends(UserEntity user, IFriendRepository friendService)
    {
        IEnumerable<FriendEntity> friendList = friendService.Execute(
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
                    .ReceiveMessage($"User {user.FirstName} {user.Lastname} has connected.");
            }
        }
    }
}
