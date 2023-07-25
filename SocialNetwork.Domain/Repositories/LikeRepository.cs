using SocialNetwork.Domain.Commands.Like;
using SocialNetwork.Tools.Cqs.Commands;

namespace SocialNetwork.Domain.Repositories;

public interface ILikeRepository :
    ICommandHandler<LikeCommand>,
    ICommandHandler<DeleteLikeCommand> { }
