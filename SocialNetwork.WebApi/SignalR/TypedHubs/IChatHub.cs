namespace SocialNetwork.WebApi.SignalR.TypedHubs;

public interface IChatHub
{
    Task ReceiveMessage(string message);
}
