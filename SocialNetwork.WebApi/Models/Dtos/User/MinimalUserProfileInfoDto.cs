namespace SocialNetwork.WebApi.Models.Dtos.User;

public class MinimalUserProfileInfoDto
{
    public int Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string? ProfilePicture { get; init; }
    
    public MinimalUserProfileInfoDto(int id, string firstName, string lastName, string? profilePicture)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        ProfilePicture = profilePicture;
    }
}
