using SocialNetwork.Models;
using SocialNetwork.WebApi.Models.Dtos.Friend;

namespace SocialNetwork.WebApi.Models.Mappers;

internal static class FriendMapper
{
    internal static FriendDto ToFriendDto(this FriendModel friendModel) =>
        new(friendModel.Id,
            friendModel.RequestId,
            friendModel.ResponderId,
            friendModel.State.ToString(),
            friendModel.CreatedAt
        );

    internal static IEnumerable<FriendDto> ToFriendDto(this IEnumerable<FriendModel> friends)
    {
        List<FriendDto> result = new List<FriendDto>();

        foreach (FriendModel friend in friends)
        {
            result.Add(friend.ToFriendDto());
        }

        return result;
    }
}
