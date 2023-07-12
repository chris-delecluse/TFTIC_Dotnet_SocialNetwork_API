use social_network
go

create procedure [dbo].[CSP_RemoveFriend](
    @requestId int,
    @responderId int
)
as
begin
    delete from [Friends] where requestId = @requestId and responderId = @responderId
end