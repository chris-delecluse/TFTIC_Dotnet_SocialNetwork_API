using MediatR;

namespace SocialNetwork.Domain.Commands.Commands.User;

public class UpdateUserProfileInfoCommand : IRequest<ICommandResult>
{
    public int UserId { get; init; }
    public string? ProfilePicture { get; set; }
    public string? BackdropImage { get; set; }
    public string? Gender { get; set; }
    public DateTime? BirthDate { get; init; }
    public string? Country { get; init; }
    public string? RelationShipStatus { get; init; }

    public UpdateUserProfileInfoCommand(
        int userId,
        string? profilePicture,
        string? backdropImage,
        string? gender,
        DateTime? birthDate,
        string? country,
        string? relationShipStatus
    )
    {
        UserId = userId;
        ProfilePicture = profilePicture;
        BackdropImage = backdropImage;
        Gender = gender;
        BirthDate = birthDate;
        Country = country;
        RelationShipStatus = relationShipStatus;
    }
}
