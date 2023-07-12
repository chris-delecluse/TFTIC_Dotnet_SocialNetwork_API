using SocialNetwork.Tools.Cqs.Commands;

namespace SocialNetwork.Domain.Commands.Post;

public class PostCommand : ICommand
{
    public string Content { get; init; }
    public int UserId { get; init; }

    public PostCommand(string content, int userId)
    {
        Content = content;
        UserId = userId;
    }
}
