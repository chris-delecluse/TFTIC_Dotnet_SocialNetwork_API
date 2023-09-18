using MediatR;

namespace SocialNetwork.Domain.Commands.Commands.Chat;

public class MessageCommand : IRequest<ICommandResult>
{
    public int From { get; init; }
    public int To { get; init; }
    public string Content { get; init; }

    public MessageCommand(int from, int to, string content)
    {
        From = from;
        To = to;
        Content = content;
    }
}
