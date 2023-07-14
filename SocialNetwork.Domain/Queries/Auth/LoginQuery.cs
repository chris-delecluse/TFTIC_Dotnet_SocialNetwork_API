using SocialNetwork.Domain.Entities;
using SocialNetwork.Tools.Cqs.Queries;

namespace SocialNetwork.Domain.Queries.Auth;

public class LoginQuery : IQuery<UserEntity?>
{
    public string Email { get; init; }
    public string Password { get; init; }

    public LoginQuery(string email, string password)
    {
        Email = email;
        Password = password;
    }
}