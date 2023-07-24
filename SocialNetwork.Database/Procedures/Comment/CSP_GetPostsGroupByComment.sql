use social_network
go

create procedure [dbo].[CSP_GetPostsGroupByComment](@isDeleted bit, @offset int, @limit int)
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
    from (select *
          from Posts
          order by createdAt desc
          offset @offset rows fetch next @limit rows only) P
             left join Comments C on P.id = C.postId
             join Users U on U.id = P.userId
    where P.isDeleted = @isDeleted
end