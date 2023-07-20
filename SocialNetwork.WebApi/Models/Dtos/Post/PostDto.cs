namespace SocialNetwork.WebApi.Models.Dtos.Post;

public class PostDto
{
    public int Id { get; init; }
    public string Content { get; init; }
    public int UserId { get; init; }
    public DateTime? CreatedAt { get; init; }

    public PostDto(int id, string content, int userId, DateTime? createdAt)
    {
        Id = id;
        Content = content;
        UserId = userId;
        CreatedAt = createdAt;
    }
}
