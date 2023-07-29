using SocialNetwork.Domain.Commands.Commands.Like;

namespace SocialNetwork.Domain.Repositories.Like;

public interface ILikeRepository
{
    Task Insert(LikeCommand command);
    Task Remove(RemoveLikeCommand command);
}
