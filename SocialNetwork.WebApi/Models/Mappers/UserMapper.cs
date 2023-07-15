using SocialNetwork.Models;
using SocialNetwork.WebApi.Models.Dtos.Auth;

namespace SocialNetwork.WebApi.Models.Mappers;

internal static class UserMapper
{
    internal static LoginDto ToLoginDto(this UserEntity user, string token) =>
        new LoginDto(user.Id, user.FirstName, user.LastName, user.Email, token);
}
