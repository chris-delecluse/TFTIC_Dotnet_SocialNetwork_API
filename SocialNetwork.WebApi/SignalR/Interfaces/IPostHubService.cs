namespace SocialNetwork.WebApi.SignalR.Interfaces;

public interface IPostHubService
{
    Task NotifyNewPost<T>(T dataToSend);
    Task NotifyPostLiked<T>(T dataToSend);
    Task NotifyPostDisliked<T>(T dataToSend);
    Task NotifyPostComment<T>(T dataToSend);
}
