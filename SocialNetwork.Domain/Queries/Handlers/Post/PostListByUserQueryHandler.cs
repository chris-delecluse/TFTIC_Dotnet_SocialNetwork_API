using MediatR;
using SocialNetwork.Domain.Queries.Queries.Post;
using SocialNetwork.Domain.Repositories.Post;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Handlers.Post;

public class PostListByUserQueryHandler: IRequestHandler<PostListByUserQuery, IEnumerable<IGrouping<IPost, PostModel>>>
{
    private readonly IPostRepository _postRepository;

    public PostListByUserQueryHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository; 
    }

    public Task<IEnumerable<IGrouping<IPost, PostModel>>> Handle(PostListByUserQuery request, CancellationToken cancellationToken)
    {
        return _postRepository.Find(request);
    }
}
