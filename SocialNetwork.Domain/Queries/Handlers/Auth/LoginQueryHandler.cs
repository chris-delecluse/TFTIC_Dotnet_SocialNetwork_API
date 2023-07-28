using MediatR;
using SocialNetwork.Domain.Queries.Queries.Auth;
using SocialNetwork.Domain.Repositories.Auth;
using SocialNetwork.Domain.Shared;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Handlers.Auth;

public class LoginQueryHandler : IRequestHandler<LoginQuery, IQueryResult<UserModel>>
{
    private readonly IAuthRepository _authRepository;

    public LoginQueryHandler(IAuthRepository authRepository) { _authRepository = authRepository; }

    public async Task<IQueryResult<UserModel>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        UserModel? user = await _authRepository.GetPublicUser(request);

        return user is not null
            ? QueryResult<UserModel>.Success(user)
            : QueryResult<UserModel>.Failure("Invalid credentials.");
    }
}
