using MediatR;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Queries.Auth;

public class LoginQuery: IRequest<UserModel?>
{
    public string Email { get; init; }
    public string Password { get; init; }

    public LoginQuery(string email, string password)
    {
        Email = email;
        Password = password;
    }
}
