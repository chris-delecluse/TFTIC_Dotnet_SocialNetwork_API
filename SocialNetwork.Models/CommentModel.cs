namespace SocialNetwork.Models;

public class CommentModel
{
    public int Id { get; init; }
    public string Content { get; init; }
    public DateTime CreatedAt { get; init; }
    public int UserId { get; init; }

    public IPost Posts { get; init; }

    public CommentModel(
        int id,
        string content,
        DateTime createdAt,
        int userId,
        int postId,
        string postContent,
        DateTime postCreatedAt,
        int postUserId
    )
    {
        Id = id;
        Content = content;
        CreatedAt = createdAt;
        UserId = userId;

        Posts = new Post(postId, postContent, postCreatedAt, postUserId);
    }

    private struct Post : IPost
    {
        public int Id { get; init; }
        public string Content { get; init; }
        public DateTime CreatedAt { get; init; }
        public int UserId { get; init; }

        public Post(int id, string content, DateTime createdAt, int userId)
        {
            Id = id;
            Content = content;
            CreatedAt = createdAt;
            UserId = userId;
        }
    }
}

public interface IPost
{
    public int Id { get; }
    public string Content { get; }
    public DateTime CreatedAt { get; }
    public int UserId { get; }
}
