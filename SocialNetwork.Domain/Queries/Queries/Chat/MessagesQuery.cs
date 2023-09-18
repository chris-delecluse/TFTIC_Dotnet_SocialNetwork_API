using MediatR;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Queries.Chat;

public class MessagesQuery: IRequest<IEnumerable<MessageModel>>
{
    public int From { get; init; }
    public int To { get; init; }
    public int? Offset { get; init; }
    public int? Limit { get; init; }

    public MessagesQuery(int from, int to, int? offset = 1, int? limit = 10)
    {
        From = from;
        To = to;
        Offset = offset;
        Limit = limit;
    }
}
