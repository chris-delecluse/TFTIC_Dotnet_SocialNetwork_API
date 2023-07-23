using System.Data;

namespace SocialNetwork.Domain.Mappers;

internal static class CommentMapper
{
    internal static int ToCommentUserId(this IDataRecord record) => (int)record["userId"];
}
