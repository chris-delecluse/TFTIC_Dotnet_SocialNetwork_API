using SocialNetwork.Tools.Cqs.Commands;

namespace SocialNetwork.Domain.Commands.Auth;

public class RegisterCommand : ICommand
{
    public string FirstName { get; init; }
    public string Lastname { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }

    public RegisterCommand(string firstName, string lastname, string email, string password)
    {
        FirstName = firstName;
        Lastname = lastname;
        Email = email;
        Password = password;
    }
}
