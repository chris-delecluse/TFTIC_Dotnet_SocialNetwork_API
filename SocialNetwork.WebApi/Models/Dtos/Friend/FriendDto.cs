using SocialNetwork.Domain.Entities;

namespace SocialNetwork.WebApi.Models.Dtos.Friend;

public class FriendDto
{
    public int Id { get; init; }
    public int RequestId { get; init; }
    public int ResponderId { get; init; }
    public string State { get; init; }
    public DateTime CreatedAt { get; init; }

    public FriendDto(int id, int requestId, int responderId, string state, DateTime createdAt)
    {
        Id = id;
        RequestId = requestId;
        ResponderId = responderId;
        State = state;
        CreatedAt = createdAt;
    }
}
