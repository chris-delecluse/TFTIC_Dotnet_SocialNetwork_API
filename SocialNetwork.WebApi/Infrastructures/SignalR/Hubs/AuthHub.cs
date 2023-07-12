using Microsoft.AspNetCore.SignalR;
using SocialNetwork.WebApi.Infrastructures.SignalR.StronglyTypedHubs;

namespace SocialNetwork.WebApi.Infrastructures.SignalR.Hubs;

public class AuthHub : Hub<IHub>
{
    [HubMethodName("AddToGroup")]
    public async Task AddToGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }
}
