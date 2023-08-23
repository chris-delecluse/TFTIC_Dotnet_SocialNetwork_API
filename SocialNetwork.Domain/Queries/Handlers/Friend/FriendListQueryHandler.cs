using MediatR;
using SocialNetwork.Domain.Queries.Queries.Friend;
using SocialNetwork.Domain.Repositories.Friend;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Handlers.Friend;

public class FriendListQueryHandler : IRequestHandler<FriendListQuery, IEnumerable<FriendModel>>
{
    private readonly IFriendRepository _friendRepository;

    public FriendListQueryHandler(IFriendRepository friendRepository)
    {
        _friendRepository = friendRepository;
    }

    public Task<IEnumerable<FriendModel>> Handle(FriendListQuery request, CancellationToken cancellationToken)
    {
        return _friendRepository.Find(request);
    }
}
