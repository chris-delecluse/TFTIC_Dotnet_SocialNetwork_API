use social_network;
go

create procedure [dbo].[CSP_GetFullProfile](
    @userId int
)
as
begin
    select u.id,
           u.firstname,
           u.lastName,
           u.email,
           up.profilePicture,
           up.backdropImage,
           up.gender,
           up.birthDate,
           up.relationShipStatus,
           up.country
    from [Users] as u
             inner join [UserProfiles] as up on u.id = up.userId
    where u.id = @userId
end
