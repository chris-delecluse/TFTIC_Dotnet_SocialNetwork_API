using SocialNetwork.Domain.Commands.Commands.Like;
using SocialNetwork.Tools.Cqs.Shared;

namespace SocialNetwork.Domain.Repositories.Like;

public interface ILikeRepository
{
    Task<ICommandResult> Insert(LikeCommand command);
    Task<ICommandResult> Remove(RemoveLikeCommand command);
}
