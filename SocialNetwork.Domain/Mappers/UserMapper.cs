using System.Data;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Domain.Mappers;

internal static class UserMapper
{
    internal static UserEntity ToPublicUser(this IDataRecord record) =>
        new((int)record["id"],
            (string)record["firstname"],
            (string)record["lastname"],
            (string)record["email"]
        );
}