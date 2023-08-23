using SocialNetwork.Models;
using SocialNetwork.WebApi.Models.Dtos.Friend;

namespace SocialNetwork.WebApi.Models.Mappers;

internal static class FriendMapper
{
    internal static FriendRequestDto ToFriendRequestDto(this FriendRequestModel friendRequestModel) =>
        new(friendRequestModel.Id,
            friendRequestModel.RequestId,
            friendRequestModel.ResponderId,
            friendRequestModel.State.ToString(),
            friendRequestModel.CreatedAt
        );

    internal static IEnumerable<FriendRequestDto> ToFriendRequestDto(this IEnumerable<FriendRequestModel> friends)
    {
        List<FriendRequestDto> result = new List<FriendRequestDto>();

        foreach (FriendRequestModel friend in friends)
        {
            result.Add(friend.ToFriendRequestDto());
        }

        return result;
    }

    internal static FriendDto ToFriendDto(this FriendModel friend) => 
        new(friend.Id, friend.FirstName, friend.LastName);
    
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
