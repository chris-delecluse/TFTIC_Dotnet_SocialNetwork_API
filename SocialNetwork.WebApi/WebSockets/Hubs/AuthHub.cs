using Microsoft.AspNetCore.SignalR;
using SocialNetwork.WebApi.WebSockets.StronglyTypedHubs;

namespace SocialNetwork.WebApi.WebSockets.Hubs;

public class AuthHub : Hub<IHub>
{
    [HubMethodName("JoinGroup")]
    public async Task JoinGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }
}
