using Microsoft.AspNetCore.SignalR;

namespace SocialNetwork.WebApi.WebSockets.Bases;

public abstract class GroupMessageHubTools
{
    protected void SendGroupMessage<THub>(string groupName, int targetId, string message, IHubContext<THub, IBaseHub> hubContext)
        where THub : Hub<IBaseHub>
    {
        SendGroupMessage($"{groupName}_{targetId}", message, hubContext);
    }

    protected void SendGroupMessage<THub>(string groupName, string message, IHubContext<THub, IBaseHub> hubContext)
        where THub : Hub<IBaseHub>
    {
        hubContext.Clients.Group(groupName)
            .JoinGroup(groupName);

        hubContext.Clients.Group(groupName)
            .ReceiveMessage(message);
    }
}
