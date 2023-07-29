using SocialNetwork.Domain.Commands.Commands.Post;
using SocialNetwork.Domain.Queries.Queries.Post;
using SocialNetwork.Domain.Shared;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Repositories.Post;

public interface IPostRepository
{
    Task<int> Insert(PostCommand command);
    Task Update(UpdatePostCommand command);
    Task<IEnumerable<IGrouping<IPost, PostModel>>> Find(PostListQuery query);
    Task<IEnumerable<IGrouping<IPost, PostModel>>> Find(PostQuery query);
}
