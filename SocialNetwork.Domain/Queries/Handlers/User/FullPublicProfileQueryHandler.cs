using MediatR;
using SocialNetwork.Domain.Queries.Queries.User;
using SocialNetwork.Domain.Repositories.User;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Handlers.User;

public class FullPublicProfileQueryHandler: IRequestHandler<FullPublicProfileQuery, UserProfileModel?>
{
    private readonly IUserRepository _userRepository;

    public FullPublicProfileQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserProfileModel?> Handle(FullPublicProfileQuery request, CancellationToken cancellationToken)
    {
       return await _userRepository.Find(request);
    }
}
