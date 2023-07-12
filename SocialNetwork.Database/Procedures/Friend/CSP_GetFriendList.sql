use social_network
go

create procedure [dbo].[CSP_GetFriendList](
    @requestId int
)
as
begin
    select * from [Friends] where requestId = @requestId or responderId = @requestId
end