using MediatR;
using SocialNetwork.Domain.Commands.Commands.Chat;
using SocialNetwork.Domain.Repositories.Chat;

namespace SocialNetwork.Domain.Commands.Handlers.Chat;

public class MessageCommandHandler: IRequestHandler<MessageCommand, ICommandResult>
{
    private readonly IChatRepository _chatRepository;

    public MessageCommandHandler(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository; 
    }

    public async Task<ICommandResult> Handle(MessageCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _chatRepository.Insert(request);
            return CommandResult.Success();
        }
        catch (Exception e)
        {
            return CommandResult.Failure("");
        }
    }
}
