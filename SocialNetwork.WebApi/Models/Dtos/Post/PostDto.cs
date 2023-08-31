namespace SocialNetwork.WebApi.Models.Dtos.Post;

public class PostDto
{
    public int Id { get; init; }
    public string Content { get; init; }
    public int UserId { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string ProfilePicture { get; init; }
    public int CommentCount { get; init; }
    public int LikeCount { get; init; }
    public DateTime? CreatedAt { get; init; }

    public PostDto(
        int id,
        string content,
        int userId,
        string firstName,
        string lastName,
        string profilePicture,
        int commentCount,
        int likeCount,
        DateTime? createdAt
    )
    {
        Id = id;
        Content = content;
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        CreatedAt = createdAt;
        ProfilePicture = profilePicture;
        CommentCount = commentCount;
        LikeCount = likeCount;
    }
}
