using MediatR;
using SocialNetwork.Domain.Queries.Queries.Friend;
using SocialNetwork.Models;

namespace SocialNetwork.WebApi.SignalR.Tools;

public abstract class FriendManager
{
    private readonly IMediator _mediator;

    protected FriendManager(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected Task<IEnumerable<FriendModel>> GetUserFriendList(int id)
    {
        return _mediator.Send(new FriendListByStateQuery(id, EFriendState.Accepted));
    }
}
