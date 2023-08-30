using MediatR;
using SocialNetwork.Domain.Queries.Queries.User;
using SocialNetwork.Domain.Repositories.User;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Handlers.User;

public class MinimalProfileQueryHandler: IRequestHandler<MinimalProfileQuery, UserProfileModel?>
{
    private readonly IUserRepository _userRepository;

    public MinimalProfileQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserProfileModel?> Handle(MinimalProfileQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.Find(request);
    }
}
