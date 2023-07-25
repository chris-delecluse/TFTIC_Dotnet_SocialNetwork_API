use social_network
go

create procedure [dbo].[CSP_RemoveFriend](
    @requestId int,
    @responderId int
)
as
begin
    if not exists(select 1
                  from Friends
                  where (requestId = @requestId and responderId = @responderId)
                     or (requestId = @responderId and responderId = @requestId))
        begin
            throw 51002, 'Friends relationship does not exist !', 1;
        end

    begin try
        begin transaction;
        delete from [Friends] where requestId = @requestId and responderId = @responderId
        delete from [Friends] where responderId = @requestId and requestId = @responderId
        commit transaction;
    end try
    begin catch
        rollback transaction;
        throw
    end catch
end