using MediatR;
using SocialNetwork.Domain.Queries.Queries.User;
using SocialNetwork.Domain.Repositories.User;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Handlers.User;

public class MinimalUserProfileInfoQueryHandler: IRequestHandler<MinimalUserProfileInfoQuery, UserProfileModel>
{
    private readonly IUserRepository _userRepository;

    public MinimalUserProfileInfoQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserProfileModel> Handle(MinimalUserProfileInfoQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.Find(request);
    }
}
