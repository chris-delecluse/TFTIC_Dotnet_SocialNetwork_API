using SocialNetwork.Tools.Cqs.Commands;

namespace SocialNetwork.Domain.Commands.Like;

public class DisLikeCommand : ICommand
{
    public int PostId { get; init; }
    public int UserId { get; init; }

    public DisLikeCommand(int postId, int userId)
    {
        PostId = postId;
        UserId = userId;
    }
}
