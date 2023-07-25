using SocialNetwork.Tools.Cqs.Commands;

namespace SocialNetwork.Domain.Commands.Post;

public class UpdatePostCommand : ICommand
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
