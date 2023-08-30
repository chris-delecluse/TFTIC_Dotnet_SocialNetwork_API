use social_network
go

create procedure [dbo].[CSP_UpdateFriendRequestStatus](
    @requestId int,
    @responderId int,
    @state nvarchar(20)
)
as
begin
    begin try

        if exists(select 1
                  from Friends
                  where requestId = @requestId
                    and responderId = @responderId
                    and state not like 'Pending')
            begin
                throw 51003, 'The request has already been processed.', 1
            end
        begin transaction;
        update Friends
        set state = @state
        where requestId = @requestId
          and responderId = @responderId

        if (@state = 'Accepted')
            begin
                update Friends
                set state = @state
                where requestId = @responderId
                  and responderId = @requestId
            end

        if (@state = 'Rejected')
            begin
                delete from [Friends] where requestId = @requestId and responderId = @responderId
                delete from [Friends] where responderId = @requestId and requestId = @responderId
            end
        commit transaction;
    end try
    begin catch

        rollback transaction;
        throw 51003, 'Error Custom.', 1
    end catch
end
