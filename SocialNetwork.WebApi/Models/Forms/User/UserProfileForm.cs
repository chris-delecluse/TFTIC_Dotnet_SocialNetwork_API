using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.WebApi.Models.Forms.User;

public class UserProfileForm
{
    [MinLength(5) , MaxLength(150)]
    public string? ProfilePicture { get; init; }
    
    [MinLength(5) , MaxLength(150)]
    public string? BackdropImage { get; init; }
    
    [MinLength(2) , MaxLength(40)]
    public string? Gender { get; init; }
    
    public DateTime? BirthDate { get; init; }
    
    [MinLength(2) , MaxLength(75)]
    public string? Country { get; init; }
    
    [MinLength(5) , MaxLength(40)]
    public string? RelationShipStatus { get; init; }
}
