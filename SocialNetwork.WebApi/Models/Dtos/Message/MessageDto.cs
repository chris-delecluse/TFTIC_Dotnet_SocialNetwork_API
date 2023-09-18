using SocialNetwork.WebApi.Models.Dtos.Friend;

namespace SocialNetwork.WebApi.Models.Dtos.Message;

public class MessageDto
{
    public int Id { get; init; }
    public FriendDto From { get; init; }
    public FriendDto To { get; init; }
    public string Content { get; init; }
    public DateTime CreatedAt { get; init; }

    public MessageDto(int id, FriendDto from, FriendDto to, string content, DateTime createdAt)
    {
        Id = id;
        From = from;
        To = to;
        Content = content;
        CreatedAt = createdAt;
    }
}
