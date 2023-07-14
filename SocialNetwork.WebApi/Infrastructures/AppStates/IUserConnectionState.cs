namespace SocialNetwork.WebApi.Infrastructures.AppStates;

public interface IUserConnectionState
{
    IEnumerable<int> GetConnectedUsers();
    void AddUserToConnectedList(int user);
    void RemoveUserToConnectedList(int id);
}
