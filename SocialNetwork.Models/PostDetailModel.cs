namespace SocialNetwork.Models;

public class PostDetailModel
{
    public int Id { get; init; }
    public string Content { get; init; }
    public DateTime CreatedAt { get; init; }
    public int UserId { get; init; }

    public IComment Comments { get; init; }

    public PostDetailModel(
        int id,
        string content,
        DateTime createdAt,
        int userId,
        int commentId,
        string commentContent,
        DateTime commentCreatedAt,
        int commentUserId,
        int commentPostId
    )
    {
        Id = id;
        Content = content;
        CreatedAt = createdAt;
        UserId = userId;

        Comments = new Comment(commentId, commentContent, commentCreatedAt, commentUserId, commentPostId);
    }

    private struct Comment : IComment
    {
        public int CommentId { get; init; }
        public string CommentContent { get; init; }
        public DateTime CommentCreatedAt { get; init; }
        public int CommentUserId { get; init; }
        public int CommentPostId { get; init; }


        public Comment(
            int commentId,
            string commentContent,
            DateTime commentCreatedAt,
            int commentUserId,
            int commentPostId
        )
        {
            CommentId = commentId;
            CommentContent = commentContent;
            CommentCreatedAt = commentCreatedAt;
            CommentUserId = commentUserId;
            CommentPostId = commentPostId;
        }
    }
}

public interface IComment
{
    public int CommentId { get; }
    public string CommentContent { get; }
    public DateTime CommentCreatedAt { get; }
    public int CommentUserId { get; }
    public int CommentPostId { get; }
}
