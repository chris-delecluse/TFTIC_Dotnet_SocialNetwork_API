using SocialNetwork.Models;
using SocialNetwork.WebApi.Models.Dtos.Comment;

namespace SocialNetwork.WebApi.Models.Mappers;

internal static class CommentMapper
{
    internal static IEnumerable<CommentDto> ToCommentDto(this IEnumerable<CommentModel> comments)
    {
        IList<CommentDto> results = new List<CommentDto>();
        
        foreach (CommentModel model in comments)
        {
            results.Add(new CommentDto(model.Id, model.Content, model.CreatedAt, model.PostId, model.UserId));
        }

        return results;
    }
}
