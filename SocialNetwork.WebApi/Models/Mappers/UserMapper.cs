using SocialNetwork.Models;
using SocialNetwork.WebApi.Models.Dtos.Auth;

namespace SocialNetwork.WebApi.Models.Mappers;

internal static class UserMapper
{
    internal static LoginDto ToLoginDto(this UserModel userModel, string token) =>
        new LoginDto(userModel.Id, userModel.FirstName, userModel.LastName, userModel.Email, token);
}
