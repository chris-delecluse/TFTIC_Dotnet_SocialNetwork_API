using MediatR;
using SocialNetwork.Domain.Commands.Commands.Friend;
using SocialNetwork.Domain.Repositories.Friend;
using SocialNetwork.Domain.Shared;

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
        try
        {
            await _friendRepository.Update(request);
            return CommandResult.Success();
        }
        catch (Exception e)
        {
            return CommandResult.Failure(e.Message);
        }
    }
}
