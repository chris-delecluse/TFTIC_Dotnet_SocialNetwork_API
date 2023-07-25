using SocialNetwork.Tools.Cqs.Commands;

namespace SocialNetwork.Domain.Commands.Friend;

public class RemoveFriendCommand : ICommand
{
    public int RequestId { get; init; }
    public int ResponderId { get; init; }

    public RemoveFriendCommand(int requestId, int responderId)
    {
        RequestId = requestId;
        ResponderId = responderId;
    }
}
