using MediatR;
using SocialNetwork.Tools.Cqs.Shared;

namespace SocialNetwork.Domain.Commands.Commands.Post;

public class PostCommand : IRequest<ICommandResult<int>>
{
    public string Content { get; init; }
    public int UserId { get; init; }

    public PostCommand(string content, int userId)
    {
        Content = content;
        UserId = userId;
    }
}
