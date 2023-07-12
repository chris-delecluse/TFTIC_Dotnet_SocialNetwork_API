using SocialNetwork.Domain.Entities;
using SocialNetwork.WebApi.Models.Dtos.Friend;

namespace SocialNetwork.WebApi.Models.Mappers;

internal static class FriendMapper
{
    internal static FriendDto ToFriendDto(this FriendEntity friend) =>
        new(friend.Id,
            friend.RequestId,
            friend.ResponderId,
            friend.State.ToString(),
            friend.CreatedAt
        );

    internal static IEnumerable<FriendDto> ToFriendDto(this IEnumerable<FriendEntity> friends)
    {
        List<FriendDto> result = new List<FriendDto>();

        foreach (FriendEntity friend in friends)
        {
            result.Add(friend.ToFriendDto());
        }

        return result;
    }
}
