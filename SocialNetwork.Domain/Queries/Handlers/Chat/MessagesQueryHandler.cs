using MediatR;
using SocialNetwork.Domain.Queries.Queries.Chat;
using SocialNetwork.Domain.Repositories.Chat;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Handlers.Chat;

public class MessagesQueryHandler: IRequestHandler<MessagesQuery, IEnumerable<MessageModel>>
{
    private readonly IChatRepository _chatRepository;

    public MessagesQueryHandler(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository; 
    }

    public async Task<IEnumerable<MessageModel>> Handle(MessagesQuery request, CancellationToken cancellationToken)
    {
        return await _chatRepository.Find(request);
    }
}
