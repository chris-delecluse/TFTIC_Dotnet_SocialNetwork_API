namespace SocialNetwork.Tools.SignalR;

public interface IMessageHubClient
{
    Task Send(object message);
}
