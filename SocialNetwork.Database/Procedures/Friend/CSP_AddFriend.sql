use social_network
go

create procedure [dbo].[CSP_AddFriend](
    @requestId int,
    @responderId int,
    @state nvarchar(20)
)
as
begin
    if exists(select 1
              from Friends
              where (requestId = @requestId and responderId = @responderId)
                 or (requestId = @responderId and responderId = @requestId)
                  and (state = 'Pending' or state = 'Accepted'))
        begin
            throw 51001, 'Friend relationship already exists or the request is pending !',1
        end

    begin try
        begin transaction;
        insert into [Friends] (requestId, responderId, state, initiator)
        values (@requestId, @responderId, @state, 1)

        select SCOPE_IDENTITY()
            
        insert into [Friends] (requestId, responderId, state, initiator) 
        values (@responderId, @requestId, @state, 0)
        commit transaction;
    end try
    begin catch
        rollback transaction;
        throw
    end catch
end
