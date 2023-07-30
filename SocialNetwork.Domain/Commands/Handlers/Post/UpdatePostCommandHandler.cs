using MediatR;
using SocialNetwork.Domain.Commands.Commands.Post;
using SocialNetwork.Domain.Repositories.Post;

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
        try
        {
             await _postRepository.Update(request);
             return CommandResult.Success();
        }
        catch (Exception e)
        {
            return CommandResult.Failure(e.Message);
        }
    }
}
