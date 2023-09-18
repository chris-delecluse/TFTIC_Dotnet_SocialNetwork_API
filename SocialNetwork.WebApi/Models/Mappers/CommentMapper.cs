using SocialNetwork.Models;
using SocialNetwork.WebApi.Models.Dtos.Comment;

namespace SocialNetwork.WebApi.Models.Mappers;

internal static class CommentMapper
{
    internal static CommentDto ToCommentDto(this CommentModel comment) =>
        new(comment.Id,
            comment.Content,
            comment.CreatedAt,
            comment.PostId,
            comment.UserId,
            comment.CommentProfilePicture
        );

    internal static IEnumerable<CommentDto> ToCommentDto(this IEnumerable<CommentModel> comments)
    {
        IList<CommentDto> results = new List<CommentDto>();

        foreach (CommentModel model in comments)
        {
            results.Add(model.ToCommentDto());
        }

        return results;
    }
}
