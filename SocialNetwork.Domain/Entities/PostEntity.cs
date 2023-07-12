namespace SocialNetwork.Domain.Entities;

public class PostEntity
{
    public int Id { get; init; }
    public string Content { get; init; }
    public DateTime CreatedAt { get; init; }
    public int UserId { get; init; }

    public PostEntity(int id, string content, DateTime createdAt, int userId)
    {
        Id = id;
        Content = content;
        CreatedAt = createdAt;
        UserId = userId;
    }
}
