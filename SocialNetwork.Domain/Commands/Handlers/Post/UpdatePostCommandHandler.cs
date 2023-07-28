using MediatR;
using SocialNetwork.Domain.Commands.Commands.Post;
using SocialNetwork.Domain.Repositories.Post;
using SocialNetwork.Tools.Cqs.Shared;

namespace SocialNetwork.Domain.Commands.Handlers.Post;

public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, ICommandResult>
{
    private readonly IPostRepository _postRepository;

    public UpdatePostCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<ICommandResult> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        return await _postRepository.Update(request);
    }
}
