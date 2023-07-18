using Microsoft.AspNetCore.SignalR;
using SocialNetwork.WebApi.SignalR.Interfaces;

namespace SocialNetwork.WebApi.SignalR.Extensions;

public static class HubContextExtension
{
    public static async Task AddToGroup<THub, THubInterface>(
        this IHubContext<THub, THubInterface> hubContext,
        string groupName
    )
        where THub : Hub<THubInterface> where THubInterface : class, IClientHub
    {
        await hubContext.Clients.Groups(groupName)
            .JoinGroup(groupName);
    }

    public static async Task RemoveToGroup<THub, THubInterface>(
        this IHubContext<THub, THubInterface> hubContext,
        string groupName
    )
        where THub : Hub<THubInterface> where THubInterface : class, IClientHub
    {
        await hubContext.Clients.Groups(groupName)
            .LeaveGroup(groupName);
    }

    public static async Task SendMessage<THub, THubInterface>(
        this IHubContext<THub, THubInterface> hubContext,
        string groupName,
        string message
    )
        where THub : Hub<THubInterface> where THubInterface : class, IClientHub
    {
        await hubContext.Clients.Groups(groupName)
            .SendMessage(message);
    }
}
