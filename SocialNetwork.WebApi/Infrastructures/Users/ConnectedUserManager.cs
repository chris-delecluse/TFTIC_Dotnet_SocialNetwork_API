using SocialNetwork.WebApi.Models.Models;

namespace SocialNetwork.WebApi.Infrastructures.Users;

public class ConnectedUserManager : IConnectedUserManager
{
    private readonly Dictionary<int, ConnectedUser> _connectedUserDictionary = new();
    
    public ConnectedUser? this[int id]
    {
        get
        {
            if (_connectedUserDictionary.TryGetValue(id, out var user))
                return user;

            return null;
        }
    }

    public IList<ConnectedUser> GetConnectedUsers() => _connectedUserDictionary.Values.ToList();

    public void AddUserToConnectedList(int id, string contextId, string firstName, string lastName)
    {
        _connectedUserDictionary[id] = new ConnectedUser(id, contextId, firstName, lastName);
    }

    public void RemoveUserToConnectedList(int id)
    {
        _connectedUserDictionary.Remove(id);
    }
}
