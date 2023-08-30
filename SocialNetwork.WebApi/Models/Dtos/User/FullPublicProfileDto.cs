namespace SocialNetwork.WebApi.Models.Dtos.User;

public class FullPublicProfileDto
{
    public int Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string? ProfilePicture { get; init; }
    public string? BackdropImage { get; init; }
    public string? Gender { get; init; }
    public DateTime? BirthDate { get; init; }
    public string? Country { get; init; }
    public string? RelationShipStatus { get; init; }
    public string? FriendRequestStatus { get; init; }
    public bool? IsFriendRequestInitiator { get; init; }

    public FullPublicProfileDto(
        int id,
        string firstName,
        string lastName,
        string? profilePicture,
        string? backdropImage,
        string? gender,
        DateTime? birthDate,
        string? country,
        string? relationShipStatus,
        string? friendRequestStatus,
        bool? isFriendRequestInitiator
    )
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        ProfilePicture = profilePicture;
        BackdropImage = backdropImage;
        Gender = gender;
        BirthDate = birthDate;
        Country = country;
        RelationShipStatus = relationShipStatus;
        FriendRequestStatus = friendRequestStatus;
        IsFriendRequestInitiator = isFriendRequestInitiator;
    }
}
