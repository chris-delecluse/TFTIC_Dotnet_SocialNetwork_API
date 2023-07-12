using SocialNetwork.Domain.Entities;

namespace SocialNetwork.WebApi.Infrastructures.Token;

public interface ITokenService
{
    string GenerateAccessToken(UserEntity user);
    int ExtractUserIdFromToken(HttpContext httpContext);
}
