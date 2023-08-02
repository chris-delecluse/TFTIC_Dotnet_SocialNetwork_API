namespace SocialNetwork.WebApi.SignalR.TypedHubs;

public interface IAuthClientHub
{
    Task OnApiConnectedAsync(string message);
    Task OnApiDisConnectedAsync(string message);

    Task ReceiveMessage(string message);
}
