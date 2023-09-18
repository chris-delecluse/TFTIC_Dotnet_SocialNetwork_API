using MediatR;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Queries.Comment;

public class CommentByIdQuery : IRequest<CommentModel?>
{
    public int Id { get; init; }

    public CommentByIdQuery(int id)
    {
        Id = id;
    }
}
