using MediatR;
using SocialNetwork.Domain.Queries.Queries.Friend;
using SocialNetwork.Domain.Repositories.Friend;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Handlers.Friend;

public class FriendRequestQueryHandler : IRequestHandler<FriendRequestsQuery, IEnumerable<FriendRequestModel>>
{
    private readonly IFriendRepository _friendRepository;

    public FriendRequestQueryHandler(IFriendRepository friendRepository)
    {
        _friendRepository = friendRepository;
    }

    public async Task<IEnumerable<FriendRequestModel>> Handle(FriendRequestsQuery request, CancellationToken cancellationToken)
    {
        return await _friendRepository.Find(request);
    }
}
