use social_network
go

create procedure [dbo].[CSP_GetUserIdsFromCommentByPostId](@postId int)
as
begin
    select distinct userId from [Comments] where postId = @postId
end