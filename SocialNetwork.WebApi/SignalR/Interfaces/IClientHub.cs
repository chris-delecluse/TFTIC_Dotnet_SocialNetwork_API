namespace SocialNetwork.WebApi.SignalR.Interfaces;

public interface IClientHub
{
    Task SendMessage(string message);
    Task JoinGroup(string groupName);
    Task LeaveGroup(string groupName);
}