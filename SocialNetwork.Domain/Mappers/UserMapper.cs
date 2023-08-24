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

    internal static UserProfileModel ToMinimalUserProfile(this IDataRecord record) =>
        new((int)record["id"],
            (string)record["firstname"],
            (string)record["lastname"],
            Mapper.GetValueOrDefault<string>(record, "profilePicture")
        );

    internal static UserProfileModel ToUserProfile(this IDataRecord record) =>
        new((int)record["id"],
            (string)record["firstname"],
            (string)record["lastname"],
            Mapper.GetValueOrDefault<string>(record, "profilePicture"),
            Mapper.GetValueOrDefault<string>(record, "gender"),
            Mapper.GetValueOrDefault<DateTime>(record, "birthDate"),
            Mapper.GetValueOrDefault<string>(record, "country"),
            Mapper.GetValueOrDefault<string>(record, "relationShipStatus")
        );
}
