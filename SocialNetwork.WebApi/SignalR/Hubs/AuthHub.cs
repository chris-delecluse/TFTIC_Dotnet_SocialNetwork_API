using Microsoft.AspNetCore.SignalR;
using SocialNetwork.WebApi.SignalR.Interfaces;

namespace SocialNetwork.WebApi.SignalR.Hubs;

public class AuthHub : Hub<IClientHub>
{
    [HubMethodName("JoinGroup")]
    public async Task JoinGroup(string groupName) => 
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

    public async Task LeaveGroup(string groupName) =>
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
}
