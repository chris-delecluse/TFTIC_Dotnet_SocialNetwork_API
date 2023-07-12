namespace SocialNetwork.WebApi.Infrastructures.SignalR.StronglyTypedHubs;

public interface IHub
{
    Task ReceiveMessage(string message);
    Task AddToGroup(string groupName);
}
