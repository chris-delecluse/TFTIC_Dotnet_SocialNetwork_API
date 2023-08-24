namespace SocialNetwork.Models;

public class FriendModel
{
    public int Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string? ProfilePicture { get; init; }
    
    public FriendModel(int id, string firstName, string lastName, string? profilePicture)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        ProfilePicture = profilePicture;
    }
}
