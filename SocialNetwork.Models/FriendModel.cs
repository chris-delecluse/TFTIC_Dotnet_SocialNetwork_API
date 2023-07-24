namespace SocialNetwork.Models;

public class FriendModel
{
    public int Id { get; init; }
    public EFriendState State { get; init; }
    public int RequestId { get; init; }
    public int ResponderId { get; init; }
    public DateTime CreatedAt { get; init; }

    public FriendModel(int id, EFriendState state, int requestId, int responderId, DateTime createdAt)
    {
        Id = id;
        State = state;
        RequestId = requestId;
        ResponderId = responderId;
        CreatedAt = createdAt;
    }
}

public enum EFriendState { Pending, Accepted, Rejected }