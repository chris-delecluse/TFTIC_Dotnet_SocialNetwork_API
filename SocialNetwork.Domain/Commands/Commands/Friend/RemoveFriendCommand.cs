using MediatR;
using SocialNetwork.Domain.Shared;

namespace SocialNetwork.Domain.Commands.Commands.Friend;

public class RemoveFriendCommand: IRequest<ICommandResult>
{
    public int RequestId { get; init; }
    public int ResponderId { get; init; }

    public RemoveFriendCommand(int requestId, int responderId)
    {
        RequestId = requestId;
        ResponderId = responderId;
    } 
}
