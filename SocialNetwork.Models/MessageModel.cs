namespace SocialNetwork.Models;

public class MessageModel
{
    public int Id { get; init; }
    public FriendModel From { get; init; }
    public FriendModel To { get; init; }
    public string Content { get; init; }
    public DateTime CreatedAt { get; init; }

    public MessageModel(
        int id,
        FriendModel from,
        FriendModel to,
        string content,
        DateTime createdAt
    )
    {
        Id = id;
        From = from;
        To = to;
        Content = content;
        CreatedAt = createdAt;
    }
}
