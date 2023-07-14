using SocialNetwork.Models;
using SocialNetwork.Tools.Cqs.Commands;

namespace SocialNetwork.Domain.Commands.Friend;

public class UpdateFriendStateCommand: ICommand
{
    public int RequestId { get; init; }
    public int ResponderId { get; init; }
    public EFriendState State { get; init; }

    public UpdateFriendStateCommand(int requestId, int responderId, EFriendState state)
    {
        RequestId = requestId;
        ResponderId = responderId;
        State = state;
    }
}
