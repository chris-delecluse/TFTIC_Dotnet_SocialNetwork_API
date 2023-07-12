using SocialNetwork.Tools.Cqs.Commands;

namespace SocialNetwork.Domain.Commands.Like;

public class LikeCommand : ICommand
{
    public int PostId { get; init; }
    public int UserId { get; init; }

    public LikeCommand(int postId, int userId)
    {
        PostId = postId;
        UserId = userId;
    }
}
