use social_network
go

create procedure [dbo].[CSP_RemoveLike](@postId int, @userId int)
as
begin
    delete Likes where postId = @postId and userId = @userId
end