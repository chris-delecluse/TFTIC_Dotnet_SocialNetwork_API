using MediatR;
using SocialNetwork.Domain.Queries.Queries.User;
using SocialNetwork.Domain.Repositories.User;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Queries.Handlers.User;

public class MinimalProfilesQueryHandler: IRequestHandler<MinimalProfilesQuery, IEnumerable<UserProfileModel>>
{
    private readonly IUserRepository _userRepository;

    public MinimalProfilesQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<IEnumerable<UserProfileModel>> Handle(MinimalProfilesQuery request, CancellationToken cancellationToken)
    {
        return _userRepository.Find(request);
    }
}
