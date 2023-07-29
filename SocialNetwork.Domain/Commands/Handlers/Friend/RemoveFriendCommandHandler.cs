using MediatR;
using SocialNetwork.Domain.Commands.Commands.Friend;
using SocialNetwork.Domain.Repositories.Friend;
using SocialNetwork.Domain.Shared;

namespace SocialNetwork.Domain.Commands.Handlers.Friend;

public class RemoveFriendCommandHandler : IRequestHandler<RemoveFriendCommand, ICommandResult>
{
    private readonly IFriendRepository _friendRepository;
    
    public RemoveFriendCommandHandler(IFriendRepository friendRepository)
    {
        _friendRepository = friendRepository;
    }

    public async Task<ICommandResult> Handle(RemoveFriendCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _friendRepository.Remove(request);
            return CommandResult.Success();
        }
        catch (Exception e)
        {
            return CommandResult.Failure(e.Message);
        }
    }
}
