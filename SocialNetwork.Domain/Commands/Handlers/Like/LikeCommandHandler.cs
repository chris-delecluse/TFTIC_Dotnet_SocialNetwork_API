using MediatR;
using SocialNetwork.Domain.Commands.Commands.Like;
using SocialNetwork.Domain.Repositories.Like;
using SocialNetwork.Tools.Cqs.Shared;

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
        return await _likeRepository.Insert(request);
    }
}
