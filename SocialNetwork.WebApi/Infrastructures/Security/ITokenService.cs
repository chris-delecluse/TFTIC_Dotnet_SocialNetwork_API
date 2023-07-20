using SocialNetwork.Models;

namespace SocialNetwork.WebApi.Infrastructures.Security;

public interface ITokenService
{
    string GenerateAccessToken(UserModel userModel);
}
