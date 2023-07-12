using SocialNetwork.Domain.Commands.Friend;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Queries.Friend;
using SocialNetwork.Tools.Cqs.Commands;
using SocialNetwork.Tools.Cqs.Queries;

namespace SocialNetwork.Domain.Repositories;

public interface IFriendRepository :
    ICommandHandler<FriendCommand>,
    ICommandHandler<UpdateFriendStateCommand>,
    IQueryHandler<FriendListQuery, IEnumerable<FriendEntity>>,
    IQueryHandler<FriendListByStateQuery, IEnumerable<FriendEntity>> { }
