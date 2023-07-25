using SocialNetwork.WebApi.Infrastructures.Security;

namespace SocialNetwork.WebApi.SignalR.Interfaces;

public interface IPostHubService
{
    Task NotifyNewPostToFriends<T>(UserInfo user, T dataToSend);
    Task NotifyLikeToPost<T>(int postId, T dataToSend);
}
