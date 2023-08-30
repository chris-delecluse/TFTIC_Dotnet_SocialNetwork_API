using SocialNetwork.Models;
using SocialNetwork.WebApi.Models.Dtos.Auth;
using SocialNetwork.WebApi.Models.Dtos.User;

namespace SocialNetwork.WebApi.Models.Mappers;

internal static class UserMapper
{
    internal static LoginDto ToLoginDto(this UserModel userModel, string token) =>
        new(userModel.Id, userModel.FirstName, userModel.LastName, userModel.Email, token);

    internal static MinimalProfileDto ToMinimalProfileDto(this UserProfileModel userProfileModel) =>
        new(userProfileModel.UserId,
            userProfileModel.FirstName,
            userProfileModel.LastName,
            userProfileModel.ProfilePicture
        );

    internal static IEnumerable<MinimalProfileDto> ToMinimalProfileDto(
        this IEnumerable<UserProfileModel> userProfileModels
    )
    {
        List<MinimalProfileDto> dto = new List<MinimalProfileDto>();

        foreach (UserProfileModel model in userProfileModels)
        {
            dto.Add(model.ToMinimalProfileDto());
        }

        return dto;
    }

    internal static FullPrivateProfileDto ToFullProfileDto(this UserProfileModel userProfileModel) =>
        new(userProfileModel.UserId,
            userProfileModel.FirstName,
            userProfileModel.LastName,
            userProfileModel.ProfilePicture,
            userProfileModel.BackdropImage,
            userProfileModel.Gender,
            userProfileModel.BirthDate,
            userProfileModel.Country,
            userProfileModel.RelationShipStatus
        );

    internal static FullPublicProfileDto ToFullPublicProfileDto(this UserProfileModel userProfileModel) =>
        new(userProfileModel.UserId,
            userProfileModel.FirstName,
            userProfileModel.LastName,
            userProfileModel.ProfilePicture,
            userProfileModel.BackdropImage,
            userProfileModel.Gender,
            userProfileModel.BirthDate,
            userProfileModel.Country,
            userProfileModel.RelationShipStatus,
            userProfileModel.FriendRequestStatus,
            userProfileModel.IsFriendRequestInitiator
        );
}
