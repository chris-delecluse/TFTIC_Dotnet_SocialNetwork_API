using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.WebApi.Models.Forms.Friend;

public class UpdateFriendRequestForm
{
    [Required(ErrorMessage = "IsAccepted field is required.")]
    public bool IsAccepted { get; init; }
    
    [Required(ErrorMessage = "RequestId field is required for the user who requested the friend invitation.")]
    public int RequestId { get; init; }
}
