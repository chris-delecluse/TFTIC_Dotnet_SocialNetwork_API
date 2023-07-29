using MediatR;
using SocialNetwork.Domain.Commands.Commands.Post;
using SocialNetwork.Domain.Repositories.Post;
using SocialNetwork.Domain.Shared;

namespace SocialNetwork.Domain.Commands.Handlers.Post;

public class PostCommandHandler : IRequestHandler<PostCommand, ICommandResult<int>>
{
    private readonly IPostRepository _postRepository;

    public PostCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<ICommandResult<int>> Handle(PostCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int result = await _postRepository.Insert(request);
            return CommandResult<int>.Success(result);
        }
        catch (Exception e)
        {
            return CommandResult<int>.Failure(e.Message);
        }
    }
}
