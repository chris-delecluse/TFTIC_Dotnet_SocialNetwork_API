use social_network
go

create procedure [dbo].[CSP_GetCommentListByPost](@postId int)
as
begin
    select C.id,
           content,
           createdAt,
           postId,
           userId
    from Comments C
    where postId = @postId;
end