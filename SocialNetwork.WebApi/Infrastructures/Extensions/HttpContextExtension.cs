using System.Security.Claims;
using SocialNetwork.WebApi.Models.Models;

namespace SocialNetwork.WebApi.Infrastructures.Extensions;

internal static class HttpContextServiceExtension
{
    internal static TokenUserInfo ExtractDataFromToken(this HttpContext httpContext)
    {
        return new TokenUserInfo(int.Parse(httpContext.User.FindFirstValue("Id")),
            httpContext.User.FindFirstValue(ClaimTypes.GivenName),
            httpContext.User.FindFirstValue(ClaimTypes.Surname)
        );
    }
}