using SocialNetwork.Domain.Commands.Friend;
using SocialNetwork.Domain.Queries.Friend;
using SocialNetwork.Models;
using SocialNetwork.Tools.Cqs.Commands;
using SocialNetwork.Tools.Cqs.Queries;

namespace SocialNetwork.Domain.Repositories;

public interface IFriendRepository :
    ICommandHandler<FriendCommand>,
    ICommandHandler<UpdateFriendStateCommand>,
    IQueryHandler<FriendListQuery, IEnumerable<FriendModel>>,
    IQueryHandler<FriendListByStateQuery, IEnumerable<FriendModel>> { }
