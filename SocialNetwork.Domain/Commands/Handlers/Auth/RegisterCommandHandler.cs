    using MediatR;
    using SocialNetwork.Domain.Commands.Commands.Auth;
    using SocialNetwork.Domain.Repositories.Auth;

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
            try
            {
                await _authRepository.RegisterUser(request);
                return CommandResult.Success("User created successfully.");
            }
            catch (Exception)
            {
                return CommandResult.Failure($"{nameof(request.Email)} is already used.");
            }
        }
    }
