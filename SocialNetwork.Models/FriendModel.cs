namespace SocialNetwork.Models;

public class FriendModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    //public string? ProfilePicture { get; set; }
    // public string SignalRContextId { get; set; }
    public FriendModel(int id, string firstName, string lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }
}
