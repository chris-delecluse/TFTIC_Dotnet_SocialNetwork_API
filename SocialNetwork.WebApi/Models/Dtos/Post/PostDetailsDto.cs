using SocialNetwork.WebApi.Models.Dtos.Comment;

namespace SocialNetwork.WebApi.Models.Dtos.Post;

public class PostDetailsDto
{
    public PostDto Post { get; set; }
    public IEnumerable<CommentDto>? Comments { get; set; }
    
    public PostDetailsDto(PostDto post, IEnumerable<CommentDto?> comments)
    {
        Post = post;
        Comments = comments;
    }
}
