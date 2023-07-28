using MediatR;
using SocialNetwork.Domain.Commands.Commands.Like;
using SocialNetwork.Domain.Repositories.Like;
using SocialNetwork.Tools.Cqs.Shared;

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
        return await _likeRepository.Remove(request);
    }
}
