using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.WebApi.Models.Forms.Auth;

#nullable disable
public class LoginForm
{
    [Required]
    public string Email { get; init; }

    [Required]
    public string Password { get; init; }
}
