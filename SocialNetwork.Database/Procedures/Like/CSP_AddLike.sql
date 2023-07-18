use social_network
go

create procedure [dbo].[CSP_AddLike](
    @postId int,
    @userId int
)
as
begin
    insert into [Likes](postId, userId)
    values (@postId, @userId)

    select SCOPE_IDENTITY()
end