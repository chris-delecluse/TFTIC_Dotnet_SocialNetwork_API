using SocialNetwork.Domain.Commands.Commands.User;
using SocialNetwork.Domain.Queries.Queries.User;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Repositories.User;

public interface IUserRepository
{
    Task Update(UpdateUserProfileInfoCommand command);
    Task<IEnumerable<UserProfileModel>> Find(MinimalProfilesQuery query);
    Task<UserProfileModel> Find(MinimalProfileQuery query);
    Task<UserProfileModel> Find(FullProfileQuery query);
    Task<UserProfileModel> Find(FullPublicProfileQuery query);
}
