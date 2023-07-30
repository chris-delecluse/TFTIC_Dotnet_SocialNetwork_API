using MediatR;
using SocialNetwork.Domain.Queries.Queries.Auth;
using SocialNetwork.Domain.Repositories.Auth;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Handlers.Auth;

public class LoginQueryHandler : IRequestHandler<LoginQuery, UserModel?>
{
    private readonly IAuthRepository _authRepository;

    public LoginQueryHandler(IAuthRepository authRepository) { _authRepository = authRepository; }

    public async Task<UserModel?> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        return await _authRepository.GetPublicUser(request);
    }
}
