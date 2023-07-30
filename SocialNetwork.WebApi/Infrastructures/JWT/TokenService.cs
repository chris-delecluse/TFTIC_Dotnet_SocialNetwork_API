using System.IdentityModel.Tokens.Jwt;
using SocialNetwork.Models;
using SocialNetwork.Tools.JWT;

namespace SocialNetwork.WebApi.Infrastructures.JWT;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateAccessToken(UserModel userModel)
    {
        var exp = DateTime.Now.AddMinutes(30);

        return new JsonWebTokenBuilder(_configuration.GetValue<string>("Jwt:Key")!)
            .AddIssuer(_configuration.GetValue<string>("Jwt:Issuer")!)
            .AddAudience(_configuration.GetValue<string>("Jwt:Audience")!)
            .AddClaim("Id", userModel.Id.ToString())
            .AddClaim(JwtRegisteredClaimNames.GivenName, userModel.FirstName)
            .AddClaim(JwtRegisteredClaimNames.FamilyName, userModel.LastName)
            .AddClaim(JwtRegisteredClaimNames.Email, userModel.Email)
            .AddClaim(JwtRegisteredClaimNames.Sub, "social network api by diwa")
            .AddClaim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            .SetExpiration(exp)
            .Build();
    }
}