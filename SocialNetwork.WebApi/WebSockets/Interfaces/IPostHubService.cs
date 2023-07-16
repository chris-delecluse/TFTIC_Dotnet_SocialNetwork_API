using SocialNetwork.WebApi.Infrastructures.Security;

namespace SocialNetwork.WebApi.WebSockets.Interfaces;

public interface IPostHubService
{
    void NotifyNewPostToFriends(UserInfo user);
}
