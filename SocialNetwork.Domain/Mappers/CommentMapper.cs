using System.Data;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Mappers;

internal static class CommentMapper
{
    internal static int ToCommentUserId(this IDataRecord record) => (int)record["userId"];

    internal static CommentModel ToComment(this IDataRecord record) =>
        new((int)record["id"],
            (string)record["content"],
            (DateTime)record["createdAt"],
            (int)record["postId"],
            (int)record["userId"],
            Mapper.GetValueOrDefault<string>(record, "commentProfilePicture")
        );
}
