using Microsoft.AspNetCore.SignalR;
using SocialNetwork.WebApi.SignalR.TypedHubs;

namespace SocialNetwork.WebApi.SignalR.Hubs;

public sealed class AuthHub : Hub<IAuthClientHub>
{
    // private readonly IConnectedUserManager _connectedUserManager;
    //
    // public AuthHub(IConnectedUserManager connectedUserManager) { _connectedUserManager = connectedUserManager; }
    //
    // public Task OnApiConnectedAsync()
    // {
    //     // attention tout le temps null ici
    //     TokenUserInfo? user = Context.GetHttpContext()
    //         ?.ExtractDataFromToken();
    //
    //     if (user is not null)
    //         _connectedUserManager.AddUserToConnectedList(user.Id, Context.ConnectionId, user.FirstName, user.LastName);
    //
    //     return Task.CompletedTask;
    // }
    //
    // public Task OnApiDisconnectedAsync()
    // {
    //     TokenUserInfo? user = Context.GetHttpContext()
    //         ?.ExtractDataFromToken();
    //
    //     if (user is not null && _connectedUserManager[user.Id] is not null)
    //         _connectedUserManager.RemoveUserToConnectedList(user.Id);
    //
    //     return Task.CompletedTask;
    // }

    public async Task SendMessage(string userId)
    {
        await Clients.Client(userId)
            .ReceiveMessage($"Send message to you with user id : {userId}");
    }

    public string GetConnectionId() => Context.ConnectionId;
}
