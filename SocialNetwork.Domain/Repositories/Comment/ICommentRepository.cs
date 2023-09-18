using SocialNetwork.Domain.Commands.Commands.Comment;
using SocialNetwork.Domain.Queries.Queries.Comment;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Repositories.Comment;

public interface ICommentRepository
{
    Task<int> Insert(CommentCommand command);
    Task<IEnumerable<CommentModel>> Find(CommentListByPostQuery query);
    Task<IEnumerable<int>> Find(CommentUserIdListByPostQuery query);
    Task<CommentModel> Find(CommentByIdQuery query);
}
