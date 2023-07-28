using MediatR;
using SocialNetwork.Domain.Commands.Commands.Friend;
using SocialNetwork.Domain.Repositories.Friend;
using SocialNetwork.Tools.Cqs.Shared;

namespace SocialNetwork.Domain.Commands.Handlers.Friend;

public class UpdateFriendRequestHandler : IRequestHandler<UpdateFriendRequestCommand, ICommandResult>
{
    private readonly IFriendRepository _friendRepository;
    
    public UpdateFriendRequestHandler(IFriendRepository friendRepository)
    {
        _friendRepository = friendRepository;
    }

    public async Task<ICommandResult> Handle(UpdateFriendRequestCommand request, CancellationToken cancellationToken)
    {
        return await _friendRepository.Update(request);
    }
}
