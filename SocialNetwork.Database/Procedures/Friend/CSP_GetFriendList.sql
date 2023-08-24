use social_network;
go

create procedure [dbo].[CSP_GetFriendList](
    @userId int
)
as
begin
    select f.requestId       as userId,
           f.responderId     as friendId,
           u.firstname       as friendFirstName,
           u.lastName        as friendLastName,
           up.profilePicture as profilePicture
    from [Friends] as f
             inner join [Users] as u on f.responderId = u.id
             inner join [UserProfiles] as up on u.id = up.userId
    where f.requestId = @userId
      and f.state = 'Accepted';
end