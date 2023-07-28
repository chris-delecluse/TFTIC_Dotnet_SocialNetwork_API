using MediatR;
using SocialNetwork.Domain.Commands.Commands.Comment;
using SocialNetwork.Domain.Repositories.Comment;
using SocialNetwork.Tools.Cqs.Shared;

namespace SocialNetwork.Domain.Commands.Handlers.Comment;

public class CommentCommandHandler : IRequestHandler<CommentCommand, ICommandResult<int>>
{
    private readonly ICommentRepository _commentRepository;
    
    public CommentCommandHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<ICommandResult<int>> Handle(CommentCommand request, CancellationToken cancellationToken)
    {
        return await _commentRepository.Insert(request);
    }
}
