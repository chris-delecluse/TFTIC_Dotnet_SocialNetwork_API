using SocialNetwork.Models;
using SocialNetwork.WebApi.Infrastructures.Security;

namespace SocialNetwork.WebApi.SignalR.Interfaces;

public interface IAuthHubService
{
    void NotifyUserConnectedToFriends(UserEntity user);
    void NotifyUserDisConnectedToFriends(UserInfo user);
}
