using System.Data;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Mappers;

internal static class UserMapper
{
    internal static UserModel ToPublicUser(this IDataRecord record) =>
        new((int)record["id"],
            (string)record["firstname"],
            (string)record["lastname"],
            (string)record["email"]
        );
}
