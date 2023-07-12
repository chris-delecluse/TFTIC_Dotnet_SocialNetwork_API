using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.WebApi.Models.Forms.Like;

public class LikeForm
{
    [Required(ErrorMessage = "PostId required.")]
    public int PostId { get; init; }
}
