using MediatR;
using SocialNetwork.Domain.Shared;

namespace SocialNetwork.Domain.Commands.Commands.Auth;

public class RegisterCommand : IRequest<ICommandResult>
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
