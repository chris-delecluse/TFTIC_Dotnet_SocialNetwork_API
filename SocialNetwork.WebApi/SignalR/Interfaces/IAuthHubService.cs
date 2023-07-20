using SocialNetwork.Models;
using SocialNetwork.WebApi.Infrastructures.Security;

namespace SocialNetwork.WebApi.SignalR.Interfaces;

public interface IAuthHubService
{
    Task NotifyUserConnectedToFriends(UserModel userModel);
    Task NotifyUserDisConnectedToFriends(UserInfo user);
}
