using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Repositories;

namespace SocialNetwork.WebApi.Infrastructures.SignalR;

public interface IHubService
{
    void NotifyUserConnectionToFriends(UserEntity user, IFriendRepository friendService);
}
