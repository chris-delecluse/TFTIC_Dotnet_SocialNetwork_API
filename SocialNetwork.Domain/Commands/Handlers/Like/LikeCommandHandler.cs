using MediatR;
using SocialNetwork.Domain.Commands.Commands.Like;
using SocialNetwork.Domain.Repositories.Like;
using SocialNetwork.Domain.Shared;

namespace SocialNetwork.Domain.Commands.Handlers.Like;

public class LikeCommandHandler : IRequestHandler<LikeCommand, ICommandResult>
{
    private readonly ILikeRepository _likeRepository;

    public LikeCommandHandler(ILikeRepository likeRepository)
    {
        _likeRepository = likeRepository;
    }

    public async Task<ICommandResult> Handle(LikeCommand request, CancellationToken cancellationToken)
    {
        try
        {
             await _likeRepository.Insert(request);
             return CommandResult.Success();
        }
        catch (Exception e)
        {
            return CommandResult.Failure(e.Message);
        }
    }
}
