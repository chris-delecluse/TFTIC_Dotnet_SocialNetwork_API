using MediatR;
using SocialNetwork.Domain.Commands.Commands.Friend;
using SocialNetwork.Domain.Repositories.Friend;
using SocialNetwork.Domain.Shared;

namespace SocialNetwork.Domain.Commands.Handlers.Friend;

public class FriendCommandHandler : IRequestHandler<FriendCommand, ICommandResult>
{
    private readonly IFriendRepository _friendRepository;

    public FriendCommandHandler(IFriendRepository friendRepository)
    {
        _friendRepository = friendRepository;
    }

    public async Task<ICommandResult> Handle(FriendCommand request, CancellationToken cancellationToken)
    {
        if (request.ResponderId <= 0)
            return CommandResult.Failure(
                "The user request to befriend (for 'responderId') cannot be less than or equal to 0."
            );

        try
        { 
            await _friendRepository.Insert(request);
            return CommandResult.Success();
        }
        catch (Exception e)
        {
            return CommandResult.Failure(e.Message);
        }
    }
}
