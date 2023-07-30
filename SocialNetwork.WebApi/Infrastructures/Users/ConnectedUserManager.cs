using SocialNetwork.WebApi.Models.Models;

namespace SocialNetwork.WebApi.Infrastructures.Users;

public class ConnectedUserManager : IConnectedUserManager
{
    private readonly Dictionary<int, ConnectedUser> _connectedUserDictionary;
    
    public ConnectedUser this[int id]
    {
        get
        {
            if (_connectedUserDictionary.TryGetValue(id, out var user))
                return user;
            throw new KeyNotFoundException($"No user with ID '{id}' found.");
        }
    }

    public ConnectedUserManager()
    {
        _connectedUserDictionary = new Dictionary<int, ConnectedUser>();
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
