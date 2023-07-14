namespace SocialNetwork.WebApi.WebSockets.StronglyTypedHubs;

public interface IHub
{
    Task ReceiveMessage(string message);
    Task JoinGroup(string groupName);
}
