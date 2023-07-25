using SocialNetwork.Tools.Cqs.Commands;

namespace SocialNetwork.Domain.Commands.Like;

public class DeleteLikeCommand : ICommand
{
    public int PostId { get; init; }
    public int UserId { get; init; }

    public DeleteLikeCommand(int postId, int userId)
    {
        PostId = postId;
        UserId = userId;
    }
}
