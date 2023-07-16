namespace SocialNetwork.WebApi.WebSockets.Bases;

public interface IBaseHub
{
    Task ReceiveMessage(string message);
    Task JoinGroup(string groupName);
}
