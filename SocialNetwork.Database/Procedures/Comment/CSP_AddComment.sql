use social_network
go

create procedure [dbo].[CSP_AddComment](
    @content nvarchar(max),
    @postId int,
    @userId int
)
as
begin
    insert into [Comments] (content, postId, userId)
    values (@content, @postId, @userId)
end
