using Microsoft.AspNetCore.SignalR;
using SocialNetwork.WebApi.SignalR.StronglyTypedHubs;

namespace SocialNetwork.WebApi.SignalR.Hubs;

public class AuthHub : Hub<IHub>
{
    [HubMethodName("AddToGroup")]
    public async Task AddToGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }
}
