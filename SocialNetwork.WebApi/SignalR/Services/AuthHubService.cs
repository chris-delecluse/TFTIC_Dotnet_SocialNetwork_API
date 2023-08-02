using MediatR;
using Microsoft.AspNetCore.SignalR;
using SocialNetwork.Models;
using SocialNetwork.WebApi.Infrastructures.Users;
using SocialNetwork.WebApi.Models.Models;
using SocialNetwork.WebApi.SignalR.Tools;
using SocialNetwork.WebApi.SignalR.Interfaces;
using SocialNetwork.WebApi.SignalR.Hubs;
using SocialNetwork.WebApi.SignalR.TypedHubs;

namespace SocialNetwork.WebApi.SignalR.Services;

public class AuthHubService : FriendManager, IAuthHubService
{
    private readonly IHubContext<AuthHub, IAuthClientHub> _context;
    private readonly IConnectedUserManager _connectedUserManager;

    public AuthHubService(
        IMediator mediator,
        IHubContext<AuthHub, IAuthClientHub> context,
        IConnectedUserManager connectedUserManager
    ) :
        base(mediator)
    {
        _context = context;
        _connectedUserManager = connectedUserManager;
    }
    
    public async Task NotifyClientUserConnected(UserModel userModel)
    {
        IEnumerable<FriendModel> friends = await GetUserFriendList(userModel.Id);

        foreach (FriendModel friend in friends)
        {
            ConnectedUser? user = _connectedUserManager[friend.RequestId];
            
            if (user is not null && friend.RequestId != userModel.Id)
                await _context.Clients.Client(user.ContextId).OnApiConnectedAsync($"{userModel.FirstName} {userModel.LastName} has connected.");
        }
    }

    public async Task NotifyClientUserDisconnected(TokenUserInfo token)
    {
        IEnumerable<FriendModel> friends = await GetUserFriendList(token.Id);

        foreach (FriendModel friend in friends)
        {
            ConnectedUser? userTarget = _connectedUserManager[friend.Id];
            
            if (userTarget is not null)
                await _context.Clients.User(userTarget.ContextId).OnApiDisConnectedAsync($"{token.FirstName} {token.LastName} has connected.");
        }
    }
}
