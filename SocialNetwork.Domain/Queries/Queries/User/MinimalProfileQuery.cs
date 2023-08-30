using MediatR;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Queries.User;

public class MinimalProfileQuery : IRequest<UserProfileModel?>
{
    public int UserId { get; init; }

    public MinimalProfileQuery(int userId)
    {
        UserId = userId;
    }
}
