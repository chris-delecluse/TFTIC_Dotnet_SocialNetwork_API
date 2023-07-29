using MediatR;
using SocialNetwork.Domain.Shared;

namespace SocialNetwork.Domain.Commands.Commands.Like;

public class LikeCommand : IRequest<ICommandResult>
{
    public int PostId { get; init; }
    public int UserId { get; init; }

    public LikeCommand(int postId, int userId)
    {
        PostId = postId;
        UserId = userId;
    }
}
