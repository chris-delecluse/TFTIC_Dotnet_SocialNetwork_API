use social_network;
go

create procedure [dbo].[CSP_GetMinimalProfileList](
    @userId int
)
as
begin
    select U.id,
           U.firstName,
           U.lastName,
           UP.profilePicture
    from [Users] as U
             inner join [UserProfiles] UP on U.id = UP.userId
    where userId != @userId
end