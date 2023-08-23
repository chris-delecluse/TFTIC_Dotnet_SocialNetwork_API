use social_network
go

create procedure [dbo].[CSP_GetFriendRequest](
    @requestId int
)
as
begin
    select * from [Friends] where requestId = @requestId or responderId = @requestId
end

SELECT f.requestId,
       f.responderId,
       u.firstname AS firstName,
       u.lastName
FROM [Friends] AS f
         INNER JOIN [Users] AS u ON f.responderId = u.id
WHERE f.requestId = 1 and f.state = 'Accepted';

