using System.Security.Claims;

namespace SocialNetwork.WebApi.Infrastructures.Extensions;

internal static class HttpContextServiceExtension
{
    internal static T? ExtractDataFromToken<T>(this HttpContext httpContext, string claimName)
    {
        if (typeof(T) == typeof(int)) 
            return (T)(object)int.Parse(httpContext.User.Claims.FirstOrDefault(c => c.Type == claimName)!.Value);
        
        return (T?)(object?)httpContext.User.FindFirstValue(claimName);
    }
}
