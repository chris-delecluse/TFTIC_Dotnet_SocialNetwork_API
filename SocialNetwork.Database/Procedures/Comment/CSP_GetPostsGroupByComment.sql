use social_network
go

create procedure [dbo].[CSP_GetPostsGroupByComment]
as
begin
    select C.id        as id,
           C.content   as content,
           C.createdAt as createdAt,
           C.userId    as userId,
           P.id        as postId,
           P.content   as postContent,
           P.createdAt as postCreatedAt,
           P.userId    as postUserId,
           U.firstname as postUserFirstName,
           U.lastName  as postUserLastName
    from Comments as C
             right join Posts as P on P.id = C.postId
             join Users U on U.id = P.userId
end