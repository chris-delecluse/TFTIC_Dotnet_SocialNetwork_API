using MediatR;
using SocialNetwork.Domain.Commands.Commands.Comment;
using SocialNetwork.Domain.Repositories.Comment;

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
        try
        {
            int result =await _commentRepository.Insert(request);
            return CommandResult<int>.Success(result, "Comment created successfully.");
        }
        catch (Exception e)
        {
            return CommandResult<int>.Failure(e.Message);
        }
    }
}
