using SocialNetwork.Domain.Entities;

namespace SocialNetwork.WebApi.SignalR.Services.Auth;

public interface IAuthHubService
{
    void NotifyUserConnectionToFriends(UserEntity user);
    void NotifyUserDisConnectionToFriends(int id, string firstName, string lastName);
}
