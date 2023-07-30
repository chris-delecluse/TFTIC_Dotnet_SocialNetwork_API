using SocialNetwork.WebApi.Models.Models;

namespace SocialNetwork.WebApi.SignalR.Interfaces;

public interface IPostHubService
{
    Task NotifyNewPostToFriends<T>(TokenUserInfo tokenUser, T dataToSend);
    Task NotifyLikeToPost<T>(int postId, T dataToSend);
}
