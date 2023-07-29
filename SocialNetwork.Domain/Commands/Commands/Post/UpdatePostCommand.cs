using MediatR;
using SocialNetwork.Domain.Shared;

namespace SocialNetwork.Domain.Commands.Commands.Post;

public class UpdatePostCommand : IRequest<ICommandResult>
{
    public int Id { get; init; }
    public int UserId { get; init; }
    public int IsDeleted { get; init; }

    public UpdatePostCommand(int id, int userId, bool isDeleted)
    {
        Id = id;
        UserId = userId;
        IsDeleted = isDeleted ? 1 : 0;
    }
}
