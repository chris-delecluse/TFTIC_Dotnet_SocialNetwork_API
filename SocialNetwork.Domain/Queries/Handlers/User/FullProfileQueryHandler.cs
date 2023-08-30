using MediatR;
using SocialNetwork.Domain.Queries.Queries.User;
using SocialNetwork.Domain.Repositories.User;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Handlers.User;

public class FullProfileQueryHandler: IRequestHandler<FullProfileQuery, UserProfileModel?>
{
    private readonly IUserRepository _userRepository;

    public FullProfileQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserProfileModel?> Handle(FullProfileQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.Find(request);
    }
}
