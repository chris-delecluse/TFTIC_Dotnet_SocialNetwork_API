using MediatR;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Commands.Commands.Friend;

public class UpdateFriendRequestCommand: IRequest<ICommandResult>
{
    public int RequestId { get; init; }
    public int ResponderId { get; init; }
    public EFriendState State { get; init; }

    public UpdateFriendRequestCommand(int requestId, int responderId, EFriendState state)
    {
        RequestId = requestId;
        ResponderId = responderId;
        State = state;
    }
}
