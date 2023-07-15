
using SocialNetwork.Models;
using SocialNetwork.WebApi.Infrastructures.Security;

namespace SocialNetwork.WebApi.WebSockets.Services;

public interface IHubService
{
    void NotifyUserConnectedToFriends(UserEntity user);
    void NotifyUserDisConnectedToFriends(UserInfo user);
    void NotifyNewPostToFriends(UserInfo user, int postId);
}
