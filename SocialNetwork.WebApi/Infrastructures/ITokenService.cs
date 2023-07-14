using SocialNetwork.Models;

namespace SocialNetwork.WebApi.Infrastructures;

public interface ITokenService
{
    string GenerateAccessToken(UserEntity user);
}
