namespace SocialNetwork.Models;

public class UserProfileModel
{
    public int UserId { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string? ProfilePicture { get; init; }
    public string? Gender { get; init; }
    public DateTime? BirthDate { get; init; }
    public string? Country { get; init; }
    public string? RelationShipStatus { get; init; }
    
    public UserProfileModel(
        int userId,
        string firstName,
        string lastName,
        string? profilePicture,
        string? gender,
        DateTime? birthDate,
        string? country,
        string? relationShipStatus
    )
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        ProfilePicture = profilePicture;
        Gender = gender;
        BirthDate = birthDate;
        Country = country;
        RelationShipStatus = relationShipStatus;
    }
    
    public UserProfileModel(
        int userId,
        string firstName,
        string lastName,
        string? profilePicture
    )
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        ProfilePicture = profilePicture;
    }
}
