use social_network;
go

create procedure [dbo].[CSP_UpdateUserProfileInfo](
    @userId int,
    @profilePicture nvarchar(150) = null,
    @backdropImage nvarchar(150) = null,
    @gender nvarchar(40) = null,
    @birthDate datetime2(7) = null,
    @country nvarchar(75) = null,
    @relationShipStatus nvarchar(40) = null
)
as
begin
    begin try
        update [UserProfiles]
        set profilePicture     = ISNULL(@profilePicture, profilePicture),
            backdropImage      = ISNULL(@backdropImage, backdropImage),
            gender             = ISNULL(@gender, gender),
            birthDate          = ISNULL(@birthDate, birthDate),
            country            = ISNULL(@country, country),
            relationShipStatus = ISNULL(@relationShipStatus, relationShipStatus)
        where userId = @userId
    end try
    begin catch
        throw
    end catch
end 