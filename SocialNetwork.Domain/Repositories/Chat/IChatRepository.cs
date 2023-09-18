using SocialNetwork.Domain.Commands.Commands.Chat;
using SocialNetwork.Domain.Queries.Queries.Chat;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Repositories.Chat;

public interface IChatRepository
{
    Task Insert(MessageCommand command);
    Task<MessageModel?> Find(MessageQuery query);
    Task<IEnumerable<MessageModel>> Find(MessagesQuery query);
}
