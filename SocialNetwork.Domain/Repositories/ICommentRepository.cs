using SocialNetwork.Domain.Commands.Comment;
using SocialNetwork.Domain.Queries.Comment;
using SocialNetwork.Tools.Cqs.Commands;
using SocialNetwork.Tools.Cqs.Queries;

namespace SocialNetwork.Domain.Repositories;

public interface ICommentRepository :
    ICommandHandler<CommentCommand>,
    IQueryHandler<CommentUserIdListByPostIdQuery, IEnumerable<int>> { }
