
using SocialNetwork.Models;

namespace SocialNetwork.WebApi.SignalR.Services;

public interface IHubService
{
    void NotifyUserConnectionToFriends(UserEntity user);
    void NotifyUserDisConnectionToFriends(int id, string firstName, string lastName);
}
