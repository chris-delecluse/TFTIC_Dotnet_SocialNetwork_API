using MediatR;
using SocialNetwork.Domain.Shared;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Queries.Auth;

public class LoginQuery: IRequest<IQueryResult<UserModel>>
{
    public string Email { get; init; }
    public string Password { get; init; }

    public LoginQuery(string email, string password)
    {
        Email = email;
        Password = password;
    }
}
