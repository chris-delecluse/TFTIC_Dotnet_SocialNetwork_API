using MediatR;
using SocialNetwork.Domain.Queries.Queries.Post;
using SocialNetwork.Domain.Repositories.Post;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Handlers.Post;

public class PostListQueryHandler : IRequestHandler<PostListQuery, IEnumerable<IGrouping<IPost, PostModel>>>
{
    private readonly IPostRepository _postRepository;

    public PostListQueryHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<IEnumerable<IGrouping<IPost, PostModel>>> Handle(PostListQuery request, CancellationToken cancellationToken)
    {
        return await _postRepository.Find(request);
    }
}
