using MediatR;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Queries.User;

public class MinimalUserProfileInfoQuery : IRequest<UserProfileModel>
{
    public int UserId { get; init; }

    public MinimalUserProfileInfoQuery(int userId)
    {
        UserId = userId;
    }
}
