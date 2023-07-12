using System.Data;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Domain.Mappers;

internal static class PostMapper
{
    internal static PostEntity ToPost(this IDataRecord record) =>
        new((int)record["id"],
            (string)record["content"],
            (DateTime)record["createdAt"],
            (int)record["userId"]
        );
}
