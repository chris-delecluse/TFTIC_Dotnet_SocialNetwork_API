using SocialNetwork.Models;
using SocialNetwork.WebApi.Models.Models;

namespace SocialNetwork.WebApi.SignalR.Interfaces;

public interface IAuthHubService
{
    Task NotifyUserConnectedToFriends(UserModel userModel);
    Task NotifyUserDisConnectedToFriends(TokenUserInfo tokenUser);
}
