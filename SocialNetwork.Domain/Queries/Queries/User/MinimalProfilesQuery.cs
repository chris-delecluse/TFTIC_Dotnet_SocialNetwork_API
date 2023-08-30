using MediatR;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Queries.User;

public class MinimalProfilesQuery : IRequest<IEnumerable<UserProfileModel>>
{
    public int UserId { get; init; }

    public MinimalProfilesQuery(int userId)
    {
        UserId = userId;
    }
}
