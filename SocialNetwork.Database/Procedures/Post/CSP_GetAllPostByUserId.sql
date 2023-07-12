use social_network
go

create procedure [dbo].[CSP_GetAllPostByUserId](
    @userId int
)
as
begin
    select *
    from [Posts]
    where userId = @userId
end