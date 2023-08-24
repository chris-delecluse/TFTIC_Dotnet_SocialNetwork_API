using SocialNetwork.Models;
using SocialNetwork.WebApi.Models.Dtos.Auth;
using SocialNetwork.WebApi.Models.Dtos.User;

namespace SocialNetwork.WebApi.Models.Mappers;

internal static class UserMapper
{
    internal static LoginDto ToLoginDto(this UserModel userModel, string token) =>
        new(userModel.Id, userModel.FirstName, userModel.LastName, userModel.Email, token);

    internal static MinimalUserProfileInfoDto ToMinimalUserProfileDto(this UserProfileModel userProfileModel) =>
        new(userProfileModel.UserId,
            userProfileModel.FirstName,
            userProfileModel.LastName,
            userProfileModel.ProfilePicture
        );

    internal static UserProfileInfoDto ToUserProfileDto(this UserProfileModel userProfileModel) =>
        new(userProfileModel.UserId,
            userProfileModel.FirstName,
            userProfileModel.LastName,
            userProfileModel.ProfilePicture,
            userProfileModel.Gender,
            userProfileModel.BirthDate,
            userProfileModel.Country,
            userProfileModel.RelationShipStatus
        );
}
