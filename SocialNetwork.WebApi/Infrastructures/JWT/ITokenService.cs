using SocialNetwork.Models;

namespace SocialNetwork.WebApi.Infrastructures.JWT;

public interface ITokenService
{
    string GenerateAccessToken(UserModel userModel);
}
