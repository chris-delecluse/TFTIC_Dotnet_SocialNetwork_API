using MediatR;
using SocialNetwork.Domain.Commands.Commands.Friend;
using SocialNetwork.Domain.Repositories.Friend;
using SocialNetwork.Tools.Cqs.Shared;

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
        return await _friendRepository.Remove(request);
    }
}
