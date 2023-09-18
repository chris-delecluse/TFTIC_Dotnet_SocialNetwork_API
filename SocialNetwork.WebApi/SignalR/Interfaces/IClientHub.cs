namespace SocialNetwork.WebApi.SignalR.Interfaces;

public interface IClientHub
{
    Task ReceiveMessage(string message);
}