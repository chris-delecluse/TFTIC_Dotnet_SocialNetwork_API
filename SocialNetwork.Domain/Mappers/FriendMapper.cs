using System.Data;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Domain.Mappers;

internal static class FriendMapper
{
    internal static FriendEntity ToFriend(this IDataRecord record) =>
        new((int)record["id"],
            (EFriendState)Enum.Parse(typeof(EFriendState), record["state"].ToString()!),
            (int)record["requestId"],
            (int)record["responderId"],
            (DateTime)record["createdAt"]
        );
}