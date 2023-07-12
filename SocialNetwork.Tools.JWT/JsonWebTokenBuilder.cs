using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SocialNetwork.Tools.JWT;

public class JsonWebTokenBuilder : IJsonWebTokenBuilder
{
    private readonly SymmetricSecurityKey _securityKey;
    private string? _issuer;
    private string? _audience;
    private List<Claim> _claims = new();
    private DateTime _expires = DateTime.Now.AddMinutes(30);

    public JsonWebTokenBuilder(string secret)
    {
        _securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
    }

    public IJsonWebTokenBuilder AddIssuer(string issuer)
    {
        _issuer = issuer;
        return this;
    }

    public IJsonWebTokenBuilder AddAudience(string audience)
    {
        _audience = audience;
        return this;
    }

    public IJsonWebTokenBuilder AddClaim(string type, string value)
    {
        _claims.Add(new Claim(type, value));
        return this;
    }

    public IJsonWebTokenBuilder AddClaim(string type, IEnumerable<string> values)
    {
        foreach (string value in values) _claims.Add(new Claim(type, value));
        return this;
    }

    public IJsonWebTokenBuilder SetExpiration(DateTime expiration)
    {
        _expires = expiration;
        return this;
    }

    public string Build()
    {
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Issuer = _issuer,
            Audience = _audience,
            Expires = _expires,
            SigningCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256Signature),
            Subject = new ClaimsIdentity(_claims)
        };

        JwtSecurityTokenHandler tokenHandler = new();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
