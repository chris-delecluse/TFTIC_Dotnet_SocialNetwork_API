using MediatR;
using SocialNetwork.Domain.Queries.Queries.User;
using SocialNetwork.Domain.Repositories.User;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Handlers.User;

public class UserProfileInfoQueryHandler: IRequestHandler<UserProfileInfoQuery, UserProfileModel>
{
    private readonly IUserRepository _userRepository;

    public UserProfileInfoQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserProfileModel> Handle(UserProfileInfoQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.Find(request);
    }
}
