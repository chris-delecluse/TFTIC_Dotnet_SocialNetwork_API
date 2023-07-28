using SocialNetwork.Domain.Commands.Commands.Auth;
using SocialNetwork.Domain.Queries.Queries.Auth;
using SocialNetwork.Domain.Shared;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Repositories.Auth;

public interface IAuthRepository
{
    Task<UserModel?> GetPublicUser(LoginQuery query);
    Task<int> RegisterUser(RegisterCommand command);
}
