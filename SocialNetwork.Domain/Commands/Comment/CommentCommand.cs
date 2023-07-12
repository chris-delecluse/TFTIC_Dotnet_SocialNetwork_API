using SocialNetwork.Tools.Cqs.Commands;

namespace SocialNetwork.Domain.Commands.Comment;

public class CommentCommand : ICommand
{
    public string Content { get; init; }
    public int PostId { get; init; }
    public int UserId { get; init; }

    public CommentCommand(string content, int postId, int userId)
    {
        Content = content;
        PostId = postId;
        UserId = userId;
    }
}
