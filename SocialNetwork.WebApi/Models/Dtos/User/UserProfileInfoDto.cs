namespace SocialNetwork.WebApi.Models.Dtos.User;

public class UserProfileInfoDto
{
    public int Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string? ProfilePicture { get; init; }
    public string? Gender { get; init; }
    public DateTime? BirthDate { get; init; }
    public string? Country { get; init; }
    public string? RelationShipStatus { get; init; }

    public UserProfileInfoDto(
        int id,
        string firstName,
        string lastName,
        string? profilePicture,
        string? gender,
        DateTime? birthDate,
        string? country,
        string? relationShipStatus
    )
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        ProfilePicture = profilePicture;
        Gender = gender;
        BirthDate = birthDate;
        Country = country;
        RelationShipStatus = relationShipStatus;
    }
}
