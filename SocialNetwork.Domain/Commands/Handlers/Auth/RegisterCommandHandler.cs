using MediatR;
using SocialNetwork.Domain.Commands.Commands.Auth;
using SocialNetwork.Domain.Repositories.Auth;
using SocialNetwork.Tools.Cqs.Shared;

namespace SocialNetwork.Domain.Commands.Handlers.Auth;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ICommandResult>
{
    private readonly IAuthRepository _authRepository;

    public RegisterCommandHandler(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<ICommandResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return await _authRepository.RegisterUser(request);
    }
}
