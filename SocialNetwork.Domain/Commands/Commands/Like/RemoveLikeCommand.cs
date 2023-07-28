using MediatR;
using SocialNetwork.Tools.Cqs.Shared;

namespace SocialNetwork.Domain.Commands.Commands.Like;

public class RemoveLikeCommand : IRequest<ICommandResult>
{
    public int PostId { get; init; }
    public int UserId { get; init; }

    public RemoveLikeCommand(int postId, int userId)
    {
        PostId = postId;
        UserId = userId;
    }
}
