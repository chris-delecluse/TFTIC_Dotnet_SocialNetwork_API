use social_network
go


create procedure [dbo].[CSP_UpdateFriendRequestStatus](
    @requestId int,
    @responderId int,
    @state nvarchar(20)
)
as
begin
    update [Friends]
    set state = @state
    where requestId = @requestId
      and responderId = @responderId
end