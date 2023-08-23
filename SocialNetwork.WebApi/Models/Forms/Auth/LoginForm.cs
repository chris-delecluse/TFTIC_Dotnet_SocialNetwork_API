using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.WebApi.Models.Forms.Auth;

#nullable disable
public class LoginForm
{
    [Required(ErrorMessage = "Invalid credentials.")]
    public string Email { get; init; }

    [Required(ErrorMessage = "Invalid credentials.")]
    public string Password { get; init; }
#nullable enable

    public string? WebSocketId { get; init; }
}
