using SocialNetwork.Domain.Entities;

namespace SocialNetwork.WebApi.Infrastructures;

public interface ITokenService
{
    string GenerateAccessToken(UserEntity user);
}
