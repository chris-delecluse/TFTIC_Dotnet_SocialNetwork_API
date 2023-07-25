use social_network
go

create procedure [dbo].[CSP_UpdateIsDeletedPost](
    @id int,
    @userId int,
    @isDeleted bit
)
as
begin
    update Posts set isDeleted = @isDeleted where id = @id and userId = @userId;
end