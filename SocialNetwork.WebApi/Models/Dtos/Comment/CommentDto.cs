namespace SocialNetwork.WebApi.Models.Dtos.Comment;

public class CommentDto
{
    public int Id { get; init; }
    public string Content { get; init; }
    public DateTime CreatedAt { get; init; }
    public int PostId { get; init; }
    public int UserId { get; init; }
    public string CommentProfilePicture { get; init; }

    public CommentDto(int id, string content, DateTime createdAt, int postId, int userId, string commentProfilePicture)
    {
        Id = id;
        Content = content;
        CreatedAt = createdAt;
        PostId = postId;
        UserId = userId;
        CommentProfilePicture = commentProfilePicture;
    }
}
