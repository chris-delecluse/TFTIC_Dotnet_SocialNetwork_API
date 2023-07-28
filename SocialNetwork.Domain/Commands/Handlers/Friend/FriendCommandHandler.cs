using MediatR;
using SocialNetwork.Domain.Commands.Commands.Friend;
using SocialNetwork.Domain.Repositories.Friend;
using SocialNetwork.Tools.Cqs.Shared;

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
            return ICommandResult.Failure(
                "The user request to befriend (for 'responderId') cannot be less than or equal to 0."
            );

        return await _friendRepository.Insert(request);
    }
}
