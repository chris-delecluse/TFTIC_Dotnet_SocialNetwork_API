using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.WebApi.Models.Forms.Auth;

#nullable disable
public class RegisterForm
{
    [Required(ErrorMessage = "FirstName required."), MinLength(2), MaxLength(50)]
    public string FirstName { get; init; }

    [Required(ErrorMessage = "LastName required."), MinLength(2), MaxLength(50)]
    public string LastName { get; init; }

    [Required(ErrorMessage = "Email required."), EmailAddress, MinLength(2), MaxLength(254)]
    public string Email { get; init; }

    [Required(ErrorMessage = "Password required."),
     RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&+=]).{6,}$",
         ErrorMessage =
             "The password must be at least 6 characters long and include at least one uppercase letter, one digit, and one special character."
     )]
    public string Password { get; init; }
}
