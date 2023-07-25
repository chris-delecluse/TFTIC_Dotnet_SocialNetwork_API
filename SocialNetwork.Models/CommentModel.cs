namespace SocialNetwork.Models;

public class CommentModel
{
    public int Id { get; init; }
    public string Content { get; init; }
    public DateTime CreatedAt { get; init; }
    public int PostId { get; init; }
    public int UserId { get; init; }

    public CommentModel(int id, string content, DateTime createdAt, int postId, int userId)
    {
        Id = id;
        Content = content;
        CreatedAt = createdAt;
        PostId = postId;
        UserId = userId;
    }
}
