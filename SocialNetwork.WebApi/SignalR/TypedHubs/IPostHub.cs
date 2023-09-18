namespace SocialNetwork.WebApi.SignalR.TypedHubs;

public interface IPostHub
{
    Task ReceiveNewPost(string message);
    Task ReceiveLike(string message);
    Task ReceiveDislike(string message);
    Task ReceiveComment(string message);
}
