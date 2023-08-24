using MediatR;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Queries.User;

public class UserProfileInfoQuery : IRequest<UserProfileModel>
{
    public int UserId { get; init; }

    public UserProfileInfoQuery(int userId)
    {
        UserId = userId;
    }
}
