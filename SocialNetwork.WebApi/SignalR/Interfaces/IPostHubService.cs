using SocialNetwork.WebApi.Infrastructures.Security;

namespace SocialNetwork.WebApi.SignalR.Interfaces;

public interface IPostHubService
{
    void NotifyNewPostToFriends<T>(UserInfo user, T dataToSend);
}
