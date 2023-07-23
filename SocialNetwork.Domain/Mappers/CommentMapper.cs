using System.Data;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Mappers;

internal static class CommentMapper
{
    internal static CommentModel ToComment(this IDataRecord record) =>
        new(Mapper.GetValueOrDefault<int>(record, "id"),
            Mapper.GetValueOrDefault<string>(record, "content"),
            Mapper.GetValueOrDefault<DateTime>(record, "createdAt"),
            Mapper.GetValueOrDefault<int>(record, "userId"),
            Mapper.GetValueOrDefault<int>(record, "postId"),
            Mapper.GetValueOrDefault<string>(record, "postContent"),
            Mapper.GetValueOrDefault<DateTime>(record, "postCreatedAt"),
            Mapper.GetValueOrDefault<int>(record, "postUserId"),
            Mapper.GetValueOrDefault<string>(record, "postUserFirstName"),
            Mapper.GetValueOrDefault<string>(record, "postUserLastName")
        );
    
    internal static int ToCommentUserId(this IDataRecord record) => (int)record["userId"];
}
