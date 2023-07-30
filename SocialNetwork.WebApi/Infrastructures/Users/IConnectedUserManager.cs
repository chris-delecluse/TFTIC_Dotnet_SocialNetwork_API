using SocialNetwork.WebApi.Models.Models;

namespace SocialNetwork.WebApi.Infrastructures.Users;

public interface IConnectedUserManager
{
    IList<ConnectedUser> GetConnectedUsers();
    ConnectedUser this[int id] { get; }
    void AddUserToConnectedList(int id, string contextId, string firstName, string lastName);
    void RemoveUserToConnectedList(int id);
}
