using MediatR;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Queries.Chat;

public class MessageQuery : IRequest<MessageModel?>
{
    public int From { get; init; }
    public int To { get; init; }
    public int? Offset { get; init; }
    public int? Limit { get; init; }

    public MessageQuery(int from, int to, int? offset = 1, int? limit = 1)
    {
        From = from;
        To = to;
        Offset = offset;
        Limit = limit;
    }
}
