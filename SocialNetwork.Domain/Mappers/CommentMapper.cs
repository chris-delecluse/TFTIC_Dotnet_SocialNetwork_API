using System.Data;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Mappers;

internal static class CommentMapper
{
    // internal static CommentModel ToComment(this IDataRecord record) =>
    //     new((int)record["id"],
    //         (string)record["content"],
    //         (DateTime)record["createdAt"],
    //         (int)record["postId"],
    //         (int)record["userId"]
    //     );

    // internal static CommentModel ToComment(this IDataRecord record) =>
    //     new((int)record["id"],
    //         (string)record["content"],
    //         (DateTime)record["createdAt"],
    //         (int)record["userId"],
    //         (int)record["postId"],
    //         (string)record["postContent"],
    //         (DateTime)record["postCreatedAt"],
    //         (int)record["postUserId"]
    //     );

    internal static CommentModel ToComment(this IDataRecord record) =>
        new(Mapper.GetValueOrDefault<int>(record, "id"),
            Mapper.GetValueOrDefault<string>(record, "content"),
            Mapper.GetValueOrDefault<DateTime>(record, "createdAt"),
            Mapper.GetValueOrDefault<int>(record, "userId"),
            Mapper.GetValueOrDefault<int>(record, "postId"),
            Mapper.GetValueOrDefault<string>(record, "postContent"),
            Mapper.GetValueOrDefault<DateTime>(record, "postCreatedAt"),
            Mapper.GetValueOrDefault<int>(record, "postUserId")
        );
    
    internal static int ToCommentUserId(this IDataRecord record) => (int)record["userId"];
}
