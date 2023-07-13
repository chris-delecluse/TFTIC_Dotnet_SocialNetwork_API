namespace SocialNetwork.WebApi.Infrastructures;

public class UserConnectionState : IUserConnectionState
{
    private readonly IList<int> _userIdConnectedList;

    public UserConnectionState()
    {
        _userIdConnectedList = new List<int>();
    }

    public IEnumerable<int> GetConnectedUsers() => _userIdConnectedList;

    public void AddUserToConnectedList(int id) => _userIdConnectedList.Add(id);

    public void RemoveUserToConnectedList(int id) => _userIdConnectedList.Remove(id);
}
