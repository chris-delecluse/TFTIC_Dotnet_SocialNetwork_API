using System.Data;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Mappers;

internal static class PostMapper
{
    internal static PostModel ToPost(this IDataRecord record) =>
        new((int)record["id"],
            (string)record["content"],
            (DateTime)record["createdAt"],
            (int)record["userId"]
        );

    internal static PostDetailModel ToPostDetail(this IDataRecord record)
    {
        return new PostDetailModel(GetValueOrDefault<int>(record, "postId"),
            GetValueOrDefault<string>(record, "postContent"),
            GetValueOrDefault<DateTime>(record, "postCreatedAt"),
            GetValueOrDefault<int>(record, "postUserId"),
            GetValueOrDefault<int>(record, "commentId"),
            GetValueOrDefault<string>(record, "commentContent"),
            GetValueOrDefault<DateTime>(record, "commentCreatedAt"),
            GetValueOrDefault<int>(record, "commentUserId"),
            GetValueOrDefault<int>(record, "commentPostId")
        );
    }

    private static T GetValueOrDefault<T>(IDataRecord record, string columnName)
    {
        return (record[columnName] != DBNull.Value ? (T)record[columnName] : default(T))!;
    }
}
