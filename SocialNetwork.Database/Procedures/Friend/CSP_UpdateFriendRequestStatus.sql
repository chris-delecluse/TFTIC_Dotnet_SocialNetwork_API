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
        begin transaction;
        if exists(select 1
                  from Friends
                  where requestId = @requestId
                    and responderId = @responderId
                    and state not like 'Pending')
            begin
                throw 51003, 'The request has already been processed.', 1
            end

        update Friends
        set state = @state
        where requestId = @requestId
          and responderId = @responderId

        if (@state = 'Accepted')
            begin
                insert into Friends (requestId, responderId, state)
                values (@responderId, @requestId, 'Accepted')
            end
        commit transaction;
    end try
    begin catch
        rollback transaction;
        throw
    end catch
end

-- use social_network
-- go
-- 
-- 
-- create procedure [dbo].[CSP_UpdateFriendRequestStatus](
--     @requestId int,
--     @responderId int,
--     @state nvarchar(20)
-- )
-- as
-- begin
--     begin try
--         begin transaction;
--         if exists(select 1
--                   from Friends
--                   where requestId = @requestId and responderId = @responderId and state not like 'Pending')
--             begin
--                 throw 51003, 'The request has already been processed.', 1;
--             end
-- 
--         update Friends
--         set state = @state
--         where requestId = @requestId and responderId = @responderId;
-- 
--         if (@state = 'Accepted')
--             begin
--                 if not exists(select 1
--                               from Friends
--                               where requestId = @responderId and responderId = @requestId and state = 'Accepted')
--                     begin
--                         insert into Friends (requestId, responderId, state)
--                         values (@responderId, @requestId, 'Accepted');
--                     end
--             end
-- 
--         commit transaction;
--     end try
--     begin catch
--         rollback transaction;
--         throw;
--     end catch
-- end
