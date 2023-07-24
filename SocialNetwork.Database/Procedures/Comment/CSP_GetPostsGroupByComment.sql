use social_network
go

create procedure [dbo].[CSP_GetPostsGroupByComment](@isDeleted bit, @offset int, @limit int)
as
begin
    WITH CTE (id, content, createdAt, userId, postId)
             AS
             (select C.id, C.content, C.createdAt, C.userId, C.postId
              from (select *,
                        /* function de fenetrage */
                           ROW_NUMBER() over (partition by postId order by createdAt desc ) as commentRowNum
                    from Comments) as C
                       join Posts as P on P.id = C.postId
              where C.commentRowNum <= 2)
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
             left join CTE C on P.id = C.postId
             join Users U on U.id = P.userId
    where P.isDeleted = @isDeleted
end
