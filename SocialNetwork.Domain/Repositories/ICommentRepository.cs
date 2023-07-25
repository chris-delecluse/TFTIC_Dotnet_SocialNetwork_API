using SocialNetwork.Domain.Commands.Comment;
using SocialNetwork.Domain.Queries.Comment;
using SocialNetwork.Models;
using SocialNetwork.Tools.Cqs.Commands;
using SocialNetwork.Tools.Cqs.Queries;

namespace SocialNetwork.Domain.Repositories;

public interface ICommentRepository :
    ICommandHandler<CommentCommand, int>,
    IQueryHandler<CommentUserIdListByPostQuery, IEnumerable<int>>,
    IQueryHandler<CommentListByPostQuery, IEnumerable<CommentModel>> { }
