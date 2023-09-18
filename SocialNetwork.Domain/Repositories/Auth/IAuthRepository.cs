using SocialNetwork.Domain.Commands.Commands.Auth;
using SocialNetwork.Domain.Queries.Queries.Auth;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Repositories.Auth;

public interface IAuthRepository
{
    Task<UserModel?> Find(LoginQuery query);
    Task<int> Insert(RegisterCommand command);
}
