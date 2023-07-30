using MediatR;

namespace SocialNetwork.Domain.Commands.Commands.Comment;

public class CommentCommand: IRequest<ICommandResult<int>>
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
