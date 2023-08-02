using SocialNetwork.Models;
using SocialNetwork.WebApi.Models.Models;

namespace SocialNetwork.WebApi.SignalR.Interfaces;

public interface IAuthHubService
{
    Task NotifyClientUserConnected(UserModel userModel);
    Task NotifyClientUserDisconnected(TokenUserInfo token);
}
