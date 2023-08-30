using MediatR;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Queries.User;

public class FullProfileQuery : IRequest<UserProfileModel?>
{
    public int UserId { get; init; }

    public FullProfileQuery(int userId)
    {
        UserId = userId;
    }
}
