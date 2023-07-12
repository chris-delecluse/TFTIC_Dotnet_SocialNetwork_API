using System.Data;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Domain.Mappers;

internal static class CommentMapper
{
    internal static CommentEntity ToComment(this IDataRecord record) =>
        new((int)record["id"],
            (string)record["content"],
            (DateTime)record["createdAt"],
            (int)record["postId"],
            (int)record["userId"]
        );
}
