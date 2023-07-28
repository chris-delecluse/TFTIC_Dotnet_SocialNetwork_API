using System.Collections;
using SocialNetwork.Domain.Commands.Commands.Post;
using SocialNetwork.Domain.Queries.Queries.Post;
using SocialNetwork.Models;
using SocialNetwork.Tools.Cqs.Shared;

namespace SocialNetwork.Domain.Repositories.Post;

public interface IPostRepository
{
    Task<ICommandResult<int>> Insert(PostCommand command);
    Task<ICommandResult> Update(UpdatePostCommand command);
    Task<IEnumerable<IGrouping<IPost, PostModel>>> Find(PostListQuery query);
    Task<IEnumerable<IGrouping<IPost, PostModel>>> Find(PostQuery query);
}
