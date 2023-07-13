namespace SocialNetwork.WebApi.Infrastructures;

public interface IUserConnectionState
{
    IEnumerable<int> GetConnectedUsers();
    void AddUserToConnectedList(int user);
    void RemoveUserToConnectedList(int id);
}
