using SocialNetwork.Domain.Entities;
using SocialNetwork.WebApi.Models.Dtos.Post;

namespace SocialNetwork.WebApi.Models.Mappers;

internal static class PostMapper
{
    internal static PostDto ToPostDto(this PostEntity post) =>
        new(post.Id,
            post.Content,
            post.CreatedAt,
            post.UserId
        );

    internal static IEnumerable<PostDto> ToPostDto(this IEnumerable<PostEntity> posts)
    {
        List<PostDto> results = new List<PostDto>();
        foreach (PostEntity post in posts)
        {
            results.Add(post.ToPostDto());
        }
        return results;
    }
}
