use social_network;
go

create procedure [dbo].[CSP_GetFriendList](
    @userId int
)
as
begin
    select f.requestId   as userId,
           f.responderId as friendId,
           u.firstname   as friendFirstName,
           u.lastName    as friendLastName
    from [Friends] as f
             inner join [Users] as u on f.responderId = u.id
    where f.requestId = @userId
      and f.state = 'Accepted';
end