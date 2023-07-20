using System.Data;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Mappers;

internal static class FriendMapper
{
    internal static FriendModel ToFriend(this IDataRecord record) =>
        new((int)record["id"],
            (EFriendState)Enum.Parse(typeof(EFriendState), record["state"].ToString()!),
            (int)record["requestId"],
            (int)record["responderId"],
            (DateTime)record["createdAt"]
        );
}
