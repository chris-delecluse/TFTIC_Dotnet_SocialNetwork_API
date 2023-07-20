using SocialNetwork.Domain.Commands.Auth;
using SocialNetwork.Domain.Queries.Auth;
using SocialNetwork.Models;
using SocialNetwork.Tools.Cqs.Commands;
using SocialNetwork.Tools.Cqs.Queries;

namespace SocialNetwork.Domain.Repositories;

public interface IAuthRepository :
    ICommandHandler<RegisterCommand>,
    IQueryHandler<LoginQuery, UserModel?>
{ }
