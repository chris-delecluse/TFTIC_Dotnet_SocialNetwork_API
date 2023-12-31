using MediatR;
using SocialNetwork.Domain.Queries.Queries.Friend;
using SocialNetwork.Domain.Repositories.Friend;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Handlers.Friend;

public class FriendListByStateQueryHandler : IRequestHandler<FriendListByStateQuery, IEnumerable<FriendRequestModel>>
{
    private readonly IFriendRepository _friendRepository;

    public FriendListByStateQueryHandler(IFriendRepository friendRepository)
    {
        _friendRepository = friendRepository;
    }

    public async Task<IEnumerable<FriendRequestModel>> Handle(FriendListByStateQuery request, CancellationToken cancellationToken)
    {
        return await _friendRepository.Find(request);
    }
}
