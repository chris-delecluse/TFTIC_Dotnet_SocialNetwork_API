namespace SocialNetwork.Tools.JWT;

public interface IJsonWebTokenBuilder
{
    IJsonWebTokenBuilder AddIssuer(string issuer);
    IJsonWebTokenBuilder AddAudience(string audience);
    IJsonWebTokenBuilder AddClaim(string type, string value);
    IJsonWebTokenBuilder AddClaim(string type, IEnumerable<string> value);
    IJsonWebTokenBuilder SetExpiration(DateTime expiration);
    string Build();
}
