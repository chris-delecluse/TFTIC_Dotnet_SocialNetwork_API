using MediatR;
using SocialNetwork.Domain.Commands.Commands.User;
using SocialNetwork.Domain.Repositories.User;

namespace SocialNetwork.Domain.Commands.Handlers.User;

public class UpdateUserProfileInfoCommandHandler: IRequestHandler<UpdateUserProfileInfoCommand, ICommandResult>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserProfileInfoCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<ICommandResult> Handle(UpdateUserProfileInfoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _userRepository.Update(request);
            return CommandResult.Success();
        }
        catch (Exception e)
        {
            return CommandResult.Failure(e.Message);
        }
    }
}
