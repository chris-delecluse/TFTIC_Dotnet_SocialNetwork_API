using SocialNetwork.Domain.Commands.Auth;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Queries.Auth;
using SocialNetwork.Tools.Cqs.Commands;
using SocialNetwork.Tools.Cqs.Queries;

namespace SocialNetwork.Domain.Repositories;

public interface IAuthRepository :
    ICommandHandler<RegisterCommand>,
    IQueryHandler<LoginQuery, UserEntity?> { }
