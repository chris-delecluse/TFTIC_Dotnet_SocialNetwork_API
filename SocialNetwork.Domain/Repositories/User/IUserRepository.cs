using SocialNetwork.Domain.Commands.Commands.User;
using SocialNetwork.Domain.Queries.Queries.User;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Repositories.User;

public interface IUserRepository
{
    Task Update(UpdateUserProfileInfoCommand command);
    Task<UserProfileModel> Find(MinimalUserProfileInfoQuery query);
    Task<UserProfileModel> Find(UserProfileInfoQuery query);
}
