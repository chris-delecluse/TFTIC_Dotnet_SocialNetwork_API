namespace SocialNetwork.WebApi.SignalR.StronglyTypedHubs;

public interface IHub
{
    Task ReceiveMessage(string message);
    Task JoinGroup(string groupName);
}
