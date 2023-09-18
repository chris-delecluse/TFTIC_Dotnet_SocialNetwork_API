using MediatR;
using SocialNetwork.Domain.Queries.Queries.Chat;
using SocialNetwork.Domain.Repositories.Chat;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Handlers.Chat;

public class MessageQueryHandler: IRequestHandler<MessageQuery, MessageModel?>
{
    private readonly IChatRepository _chatRepository;

    public MessageQueryHandler(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository; 
    }

    public async Task<MessageModel?> Handle(MessageQuery request, CancellationToken cancellationToken)
    {
        return await _chatRepository.Find(request);
    }
}
