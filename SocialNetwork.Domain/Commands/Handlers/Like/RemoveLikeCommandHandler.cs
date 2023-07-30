using MediatR;
using SocialNetwork.Domain.Commands.Commands.Like;
using SocialNetwork.Domain.Repositories.Like;

namespace SocialNetwork.Domain.Commands.Handlers.Like;

public class RemoveLikeCommandHandler : IRequestHandler<RemoveLikeCommand, ICommandResult>
{
    private readonly ILikeRepository _likeRepository;

    public RemoveLikeCommandHandler(ILikeRepository likeRepository)
    {
        _likeRepository = likeRepository;
    }

    public async Task<ICommandResult> Handle(RemoveLikeCommand request, CancellationToken cancellationToken)
    {
        try
        {
             await _likeRepository.Remove(request);
             return CommandResult.Success();
        }
        catch (Exception e)
        {
            return CommandResult.Failure(e.Message);
        }
    }
}
