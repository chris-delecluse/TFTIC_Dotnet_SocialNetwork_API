using SocialNetwork.Models;
using SocialNetwork.WebApi.Models.Dtos.Comment;
using SocialNetwork.WebApi.Models.Dtos.Post;

namespace SocialNetwork.WebApi.Models.Mappers;

internal static class PostMapper
{
    internal static PostDto ToPostDto(this PostModel postModel) =>
        new(postModel.Id,
            postModel.Content,
            postModel.UserId,
            postModel.CreatedAt
        );

    internal static IEnumerable<PostDto> ToPostDto(this IEnumerable<PostModel> posts)
    {
        List<PostDto> results = new List<PostDto>();
        foreach (PostModel post in posts) { results.Add(post.ToPostDto()); }

        return results;
    }

    internal static IEnumerable<PostDetailsDto> ToPostDetailDto(this IEnumerable<IGrouping<IPost, CommentModel>> posts)
    {
        return posts.Select(p => new PostDetailsDto(new PostDto(p.Key.Id,
                    p.Key.Content,
                    p.Key.UserId,
                    p.Key.CreatedAt
                ),
                p.Select(c => new CommentDto(c.Id, c.Content, c.CreatedAt, p.Key.Id, c.UserId))
            )
        );
    }
}
