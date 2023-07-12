use social_network
go

create procedure [dbo].[CSP_GetPostByUserId](
    @postId int,
    @userId int
)
as
begin
    select *
    from [Posts]
    where id = @postId
      and userId = @userId
end