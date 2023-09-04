namespace SocialNetwork.Models;

public class MessageModel
{
    public int Id { get; init; }
    public UserProfileModel From { get; init; }
    public UserProfileModel To { get; init; }
    public string Content { get; init; }
    public DateTime CreatedAt { get; init; }

    public MessageModel(
        int id,
        UserProfileModel from,
        UserProfileModel to,
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
