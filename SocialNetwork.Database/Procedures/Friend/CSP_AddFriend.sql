use social_network
go

create procedure [dbo].[CSP_AddFriend](
    @requestId int,
    @responderId int,
    @state nvarchar(20)
)
as
begin
    insert into [Friends] (requestId, responderId, state)
    values (@requestId, @responderId, @state)

    select SCOPE_IDENTITY()
end