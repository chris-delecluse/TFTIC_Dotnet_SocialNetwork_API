namespace SocialNetwork.WebApi.Models.Dtos.Post;

public class PostDto
{
    public int Id { get; init; }
    public string Content { get; init; }
    public DateTime CreatedAt { get; init; }
    public int UserId { get; init; }

    public PostDto(int id, string content, DateTime createdAt, int userId)
    {
        Id = id;
        Content = content;
        CreatedAt = createdAt;
        UserId = userId;
    }
}
