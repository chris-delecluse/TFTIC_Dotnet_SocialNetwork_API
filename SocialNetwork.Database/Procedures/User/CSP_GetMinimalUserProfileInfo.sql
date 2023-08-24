use social_network;
go

create procedure [dbo].[CSP_GetMinimalUserProfileInfo](
    @userId int
)
as
begin
    select u.id,
           u.firstname,
           u.lastName,
           up.profilePicture
    from [Users] as u
             inner join [UserProfiles] as up on u.id = up.userId
    where u.id = @userId
end