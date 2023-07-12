using SocialNetwork.Domain.Commands.Comment;
using SocialNetwork.Tools.Cqs.Commands;

namespace SocialNetwork.Domain.Repositories;

public interface ICommentRepository :
    ICommandHandler<CommentCommand> { }
