using MediatR;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Queries.User;

public class FullPublicProfileQuery: IRequest<UserProfileModel?>
{
    public int UserProfileId { get; init; }
    public int ViewerId { get; init; }
    
    public FullPublicProfileQuery(int userProfileId, int viewerId)
    {
        UserProfileId = userProfileId;
        ViewerId = viewerId;
    }
}
