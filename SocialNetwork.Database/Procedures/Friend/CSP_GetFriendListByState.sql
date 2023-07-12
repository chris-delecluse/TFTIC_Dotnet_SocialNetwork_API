use social_network
go

create procedure [dbo].[CSP_GetFriendListByState](
    @requestId int,
    @state nvarchar(20)
)
as
begin
    select * from [Friends] where (requestId = @requestId or responderId = @requestId) and state = @state
end