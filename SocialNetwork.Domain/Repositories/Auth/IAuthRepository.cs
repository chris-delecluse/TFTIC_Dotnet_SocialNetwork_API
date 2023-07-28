using SocialNetwork.Domain.Commands.Commands.Auth;
using SocialNetwork.Domain.Queries.Queries.Auth;
using SocialNetwork.Models;
using SocialNetwork.Tools.Cqs.Shared;

namespace SocialNetwork.Domain.Repositories.Auth;

public interface IAuthRepository
{
    Task<UserModel?> GetPublicUser(LoginQuery query);
    Task<ICommandResult> RegisterUser(RegisterCommand command);
}
