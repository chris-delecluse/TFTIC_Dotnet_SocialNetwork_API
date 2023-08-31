using SocialNetwork.Models;
using SocialNetwork.WebApi.Models.Dtos.Comment;
using SocialNetwork.WebApi.Models.Dtos.Post;

namespace SocialNetwork.WebApi.Models.Mappers;

internal static class PostMapper
{
    internal static IEnumerable<PostDetailsDto> ToPostDto(this IEnumerable<IGrouping<IPost, PostModel>> posts)
    {
        return posts.Select(p => new PostDetailsDto(new PostDto(p.Key.Id,
                    p.Key.Content,
                    p.Key.UserId,
                    p.Key.UserFirstName,
                    p.Key.UserLastName,
                    p.Key.UserProfilePicture,
                    p.Key.CommentCount,
                    p.Key.LikesCount,
                    p.Key.CreatedAt
                ),
                p.Select(c => c.Id != 0 ? new CommentDto(c.Id, c.Content, c.CreatedAt, p.Key.Id, c.UserId) : null)
                    .Where(c => c != null)
            )
        );
    }

    internal static PostDetailsDto ToPostDto(this IGrouping<IPost, PostModel> post) =>
        new(new PostDto(post.Key.Id,
                post.Key.Content,
                post.Key.UserId,
                post.Key.UserFirstName,
                post.Key.UserLastName,
                post.Key.UserProfilePicture,
                post.Key.CommentCount,
                post.Key.LikesCount,
                post.Key.CreatedAt
            ),
            post.Select(c => c.Id != 0 ? new CommentDto(c.Id, c.Content, c.CreatedAt, post.Key.Id, c.UserId) : null)
                .Where(c => c != null)
        );
}
