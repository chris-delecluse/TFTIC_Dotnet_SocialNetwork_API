using System.IdentityModel.Tokens.Jwt;
using SocialNetwork.Models;
using SocialNetwork.Tools.JWT;

namespace SocialNetwork.WebApi.Infrastructures.Security;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateAccessToken(UserEntity user)
    {
        var exp = DateTime.Now.AddMinutes(30);

        return new JsonWebTokenBuilder(_configuration.GetValue<string>("Jwt:Key")!)
            .AddIssuer(_configuration.GetValue<string>("Jwt:Issuer")!)
            .AddAudience(_configuration.GetValue<string>("Jwt:Audience")!)
            .AddClaim("Id", user.Id.ToString())
            .AddClaim(JwtRegisteredClaimNames.GivenName, user.FirstName)
            .AddClaim(JwtRegisteredClaimNames.FamilyName, user.LastName)
            .AddClaim(JwtRegisteredClaimNames.Email, user.Email)
            .AddClaim(JwtRegisteredClaimNames.Sub, "social network api by diwa")
            .AddClaim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            .SetExpiration(exp)
            .Build();
    }
}