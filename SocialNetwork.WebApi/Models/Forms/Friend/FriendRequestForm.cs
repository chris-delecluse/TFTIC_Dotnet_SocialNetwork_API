using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.WebApi.Models.Forms.Friend;

public class FriendRequestForm
{
    [Required(ErrorMessage = "The userToFriend ('userId') id is required.")]
    public int UserId { get; init; }
}
