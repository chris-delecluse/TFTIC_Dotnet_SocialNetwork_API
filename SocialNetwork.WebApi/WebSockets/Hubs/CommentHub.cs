using Microsoft.AspNetCore.SignalR;
using SocialNetwork.WebApi.WebSockets.Bases;

namespace SocialNetwork.WebApi.WebSockets.Hubs;

public class CommentHub : Hub<IBaseHub>
{
    [HubMethodName("JoinGroup")]
    public async Task JoinGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }
}
