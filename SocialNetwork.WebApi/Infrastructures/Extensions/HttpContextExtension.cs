using System.Security.Claims;
using SocialNetwork.WebApi.Infrastructures.Security;

namespace SocialNetwork.WebApi.Infrastructures.Extensions;

internal static class HttpContextServiceExtension
{
    internal static UserInfo ExtractDataFromToken(this HttpContext httpContext)
    {
        return new UserInfo(int.Parse(httpContext.User.FindFirstValue("Id")),
            httpContext.User.FindFirstValue(ClaimTypes.GivenName),
            httpContext.User.FindFirstValue(ClaimTypes.Surname)
        );
    }
}