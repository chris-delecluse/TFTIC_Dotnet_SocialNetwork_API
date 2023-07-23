using SocialNetwork.Models;
using SocialNetwork.Tools.Cqs.Queries;

namespace SocialNetwork.Domain.Queries.Post;

public class PostListQuery : IQuery<IEnumerable<IGrouping<IPost, PostModel>>>
{ 
    public int IsDeleted { get; init; }

    public PostListQuery(bool isDeleted)
    {
        IsDeleted = isDeleted ? 1 : 0;
    }
}
