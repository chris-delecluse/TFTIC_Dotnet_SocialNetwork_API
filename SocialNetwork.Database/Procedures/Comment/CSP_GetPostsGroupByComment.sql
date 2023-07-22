use social_network
go

create procedure [dbo].[CSP_GetPostsGroupByComment]
as
begin
    select C.id        as id,
           C.content   as content,
           C.createdAt as createdAt,
           C.userId    as userId,
           P.id    as postId,
           P.content   as postContent,
           P.createdAt as postCreatedAt,
           P.userId    as postUserId
    from Comments as C
            right join Posts as P on P.id = C.postId
end